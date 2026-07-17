// DebtManagementList.tsx — Customer Debt Management Dashboard
// Shows KPI summaries, aging analysis, and filterable debt list
import { useState, useMemo } from 'react';
import {
  Box, Paper, Typography, Button, Chip, Avatar, TextField, InputAdornment,
  Tooltip, IconButton, Dialog, DialogTitle, DialogContent, DialogActions,
  alpha, useTheme,
} from '@mui/material';
import Grid from '@mui/material/Grid2';
import { DataGrid, type GridColDef, type GridPaginationModel, type GridSortModel } from '@mui/x-data-grid';
import {
  BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip as RTooltip, ResponsiveContainer,
  PieChart, Pie, Cell,
} from 'recharts';
import { useNavigate } from 'react-router-dom';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import SearchIcon from '@mui/icons-material/Search';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';
import AccountBalanceIcon from '@mui/icons-material/AccountBalance';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import TrendingDownIcon from '@mui/icons-material/TrendingDown';
import PaymentIcon from '@mui/icons-material/Payment';
import { CustomerDebtApi } from '../api/CustomerDebtApi';
import { CustomerApi } from '../api/CustomerApi';
import { useEnumValues } from '../hooks/useEnumValues';
import { useNotification } from '../contexts/NotificationContext';
import useTranslation from '../i18n/useTranslation';
import type { CustomerDebt } from '../types/CustomerDebt';

const PIE_COLORS = ['#2563EB', '#D97706', '#DC2626', '#059669'];

function SummaryCard({ title, value, subtitle, icon, color }: {
  title: string; value: string; subtitle?: string; icon: React.ReactNode; color: string;
}) {
  return (
    <Paper sx={{ p: 2.5, position: 'relative', overflow: 'hidden' }}>
      <Box sx={{
        position: 'absolute', top: -15, left: -15, width: 80, height: 80,
        borderRadius: '50%', background: alpha(color, 0.08),
      }} />
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Box>
          <Typography variant="body2" color="text.secondary" fontWeight={500}>{title}</Typography>
          <Typography variant="h5" fontWeight={700} mt={0.5} dir="ltr">{value}</Typography>
          {subtitle && <Typography variant="caption" color="text.secondary">{subtitle}</Typography>}
        </Box>
        <Avatar sx={{ bgcolor: alpha(color, 0.12), color, width: 44, height: 44 }}>
          {icon}
        </Avatar>
      </Box>
    </Paper>
  );
}

export default function DebtManagementList() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const theme = useTheme();
  const queryClient = useQueryClient();
  const { showSuccess, showError } = useNotification();

  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({ page: 0, pageSize: 25 });
  const [sortModel, setSortModel] = useState<GridSortModel>([{ field: 'dueDate', sort: 'asc' }]);
  const [search, setSearch] = useState('');
  const [paymentDialog, setPaymentDialog] = useState<{ open: boolean; debt: CustomerDebt | null }>({
    open: false, debt: null,
  });
  const [paymentAmount, setPaymentAmount] = useState('');

  const debtStatusOptions = useEnumValues('DebtStatus');

  const { data, isLoading } = useQuery({
    queryKey: ['customerDebts', 'management', paginationModel, sortModel, search],
    queryFn: () => CustomerDebtApi.getAll(
      paginationModel.page, paginationModel.pageSize, search,
      sortModel[0]?.field ?? '', sortModel[0]?.sort ?? 'asc',
    ),
    staleTime: 30_000,
  });

  // Load all debts for summary calculations
  const { data: allDebts } = useQuery({
    queryKey: ['customerDebts', 'all-summary'],
    queryFn: () => CustomerDebtApi.getAll(0, 9999, ''),
    staleTime: 60_000,
  });

  // Customers for mapping fkCustomerId -> customerName
  const { data: customersData } = useQuery({
    queryKey: ['customers', 'all'],
    queryFn: () => CustomerApi.getAll(0, 9999, ''),
    staleTime: 5 * 60_000,
  });

  // Calculate summaries
  const summary = useMemo(() => {
    if (!allDebts?.items) return { total: 0, paid: 0, remaining: 0, overdue: 0, overdueCount: 0 };
    const items = allDebts.items as CustomerDebt[];
    const total = items.reduce((s, d) => s + d.debtAmount, 0);
    const paid = items.reduce((s, d) => s + (d.paidAmount ?? 0), 0);
    const remaining = total - paid;
    const today = new Date().toISOString().split('T')[0];
    const overdueItems = items.filter(d => d.dueDate && d.dueDate < today && (d.remainingAmount ?? 0) > 0);
    return { total, paid, remaining, overdue: overdueItems.reduce((s, d) => s + (d.remainingAmount ?? 0), 0), overdueCount: overdueItems.length };
  }, [allDebts]);

  // Real aging buckets by debtDate age over remainingAmount
  const agingData = useMemo(() => {
    const buckets = [
      { range: '0-30 יום', amount: 0 },
      { range: '31-60 יום', amount: 0 },
      { range: '61-90 יום', amount: 0 },
      { range: '90+ יום', amount: 0 },
    ];
    const now = Date.now();
    ((allDebts?.items ?? []) as CustomerDebt[]).forEach(d => {
      const remaining = d.remainingAmount ?? 0;
      if (remaining <= 0 || !d.debtDate) return;
      const ageDays = Math.floor((now - new Date(d.debtDate).getTime()) / 86_400_000);
      const idx = ageDays <= 30 ? 0 : ageDays <= 60 ? 1 : ageDays <= 90 ? 2 : 3;
      buckets[idx].amount += remaining;
    });
    return buckets;
  }, [allDebts]);

  // Status distribution for pie chart
  const statusDistribution = useMemo(() => {
    if (!allDebts?.items) return [];
    const counts: Record<number, number> = {};
    (allDebts.items as CustomerDebt[]).forEach(d => {
      const s = d.enmDebtStatus ?? 0;
      counts[s] = (counts[s] ?? 0) + 1;
    });
    return Object.entries(counts).map(([status, count]) => ({
      name: debtStatusOptions.find(o => o.value === Number(status))?.label ?? `סטטוס ${status}`,
      value: count,
    }));
  }, [allDebts, debtStatusOptions]);

  const formatCurrency = (n: number) => `₪${n.toLocaleString('he-IL', { minimumFractionDigits: 0 })}`;

  const columns: GridColDef[] = [
    { field: 'id', headerName: '#', width: 60 },
    {
      field: 'fkCustomerId', headerName: t.fields.customerName, flex: 1, minWidth: 150,
      renderCell: (p) => {
        const customer = customersData?.items?.find((c) => c.id === p.value);
        return <Typography variant="body2">{customer?.customerName ?? `לקוח #${p.value}`}</Typography>;
      },
    },
    {
      field: 'debtAmount', headerName: t.fields.debtAmount, width: 130,
      renderCell: (p) => <Typography dir="ltr" fontWeight={500}>{formatCurrency(p.value)}</Typography>,
    },
    {
      field: 'paidAmount', headerName: t.fields.paidAmount, width: 130,
      renderCell: (p) => (
        <Typography dir="ltr" color="success.main" fontWeight={500}>
          {formatCurrency(p.value ?? 0)}
        </Typography>
      ),
    },
    {
      field: 'remainingAmount', headerName: t.fields.remainingAmount, width: 130,
      renderCell: (p) => {
        const val = p.value ?? 0;
        return (
          <Typography dir="ltr" fontWeight={700} color={val > 0 ? 'error.main' : 'success.main'}>
            {formatCurrency(val)}
          </Typography>
        );
      },
    },
    {
      field: 'debtDate', headerName: t.fields.debtDate, width: 120,
      valueFormatter: (v: string) => v ? new Date(v).toLocaleDateString('he-IL') : '',
    },
    {
      field: 'dueDate', headerName: t.fields.dueDate, width: 120,
      renderCell: (p) => {
        if (!p.value) return '—';
        const d = new Date(p.value);
        const isOverdue = d < new Date() && (p.row.remainingAmount ?? 0) > 0;
        return (
          <Typography color={isOverdue ? 'error.main' : 'text.primary'} fontWeight={isOverdue ? 700 : 400}>
            {d.toLocaleDateString('he-IL')}
            {isOverdue && ' ⚠️'}
          </Typography>
        );
      },
    },
    {
      field: 'enmDebtStatus', headerName: t.enums.debtStatus, width: 120,
      renderCell: (p) => {
        const label = debtStatusOptions.find(o => o.value === p.value)?.label ?? '—';
        const color = p.value === 2 ? 'success' : p.value === 3 ? 'error' : 'warning';
        return <Chip size="small" label={label} color={color as any} variant="outlined" />;
      },
    },
    {
      field: 'actions', headerName: '', width: 80, sortable: false,
      renderCell: (p) => (
        <Tooltip title={t.debtManagement.recordPayment}>
          <IconButton size="small" color="primary"
            onClick={() => { setPaymentDialog({ open: true, debt: p.row }); setPaymentAmount(''); }}>
            <PaymentIcon fontSize="small" />
          </IconButton>
        </Tooltip>
      ),
    },
  ];

  // Record payment mutation
  const paymentMutation = useMutation({
    mutationFn: async () => {
      if (!paymentDialog.debt) return;
      const debt = paymentDialog.debt;
      const amount = Number(paymentAmount);
      const newPaid = (debt.paidAmount ?? 0) + amount;
      await CustomerDebtApi.update(debt.id, {
        ...debt,
        paidAmount: newPaid,
        enmDebtStatus: newPaid >= debt.debtAmount ? 2 : debt.enmDebtStatus, // 2 = Paid
      } as any);
    },
    onSuccess: () => {
      showSuccess(t.messages.updated);
      queryClient.invalidateQueries({ queryKey: ['customerDebts'] });
      setPaymentDialog({ open: false, debt: null });
    },
    onError: () => showError(t.messages.error),
  });

  return (
    <Box>
      {/* Header */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">{t.debtManagement.title}</Typography>
        <Button variant="contained" onClick={() => navigate('/customerDebts/new')}>
          + {t.entities.customerDebt.s}
        </Button>
      </Box>

      {/* KPI Cards */}
      <Grid container spacing={2} mb={3}>
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <SummaryCard title={t.debtManagement.totalDebt} value={formatCurrency(summary.total)}
            icon={<AccountBalanceIcon />} color="#2563EB" />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <SummaryCard title={t.debtManagement.totalPaid} value={formatCurrency(summary.paid)}
            icon={<CheckCircleIcon />} color="#059669" />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <SummaryCard title={t.debtManagement.totalRemaining} value={formatCurrency(summary.remaining)}
            icon={<TrendingDownIcon />} color="#D97706" />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <SummaryCard title={t.debtManagement.overdue}
            value={formatCurrency(summary.overdue)}
            subtitle={`${summary.overdueCount} חובות`}
            icon={<WarningAmberIcon />} color="#DC2626" />
        </Grid>
      </Grid>

      {/* Charts */}
      <Grid container spacing={2} mb={3}>
        <Grid size={{ xs: 12, md: 8 }}>
          <Paper sx={{ p: 3 }}>
            <Typography variant="h6" mb={2}>{t.debtManagement.aging}</Typography>
            <ResponsiveContainer width="100%" height={250}>
              <BarChart data={agingData}>
                <CartesianGrid strokeDasharray="3 3" stroke={theme.palette.divider} />
                <XAxis dataKey="range" fontSize={12} />
                <YAxis fontSize={12} tickFormatter={(v) => `₪${(v/1000).toFixed(0)}K`} />
                <RTooltip contentStyle={{
                  backgroundColor: theme.palette.background.paper,
                  border: `1px solid ${theme.palette.divider}`,
                  borderRadius: 8, direction: 'rtl',
                }} formatter={(v: number) => [formatCurrency(v), 'סכום']} />
                <Bar dataKey="amount" radius={[4, 4, 0, 0]}>
                  {[0, 1, 2, 3].map(i => <Cell key={i} fill={PIE_COLORS[i]} />)}
                </Bar>
              </BarChart>
            </ResponsiveContainer>
          </Paper>
        </Grid>
        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" mb={2}>סטטוס חובות</Typography>
            <ResponsiveContainer width="100%" height={200}>
              <PieChart>
                <Pie data={statusDistribution} cx="50%" cy="50%" innerRadius={45} outerRadius={75}
                  paddingAngle={3} dataKey="value">
                  {statusDistribution.map((_, i) => <Cell key={i} fill={PIE_COLORS[i % PIE_COLORS.length]} />)}
                </Pie>
                <RTooltip contentStyle={{
                  backgroundColor: theme.palette.background.paper,
                  borderRadius: 8, direction: 'rtl',
                }} />
              </PieChart>
            </ResponsiveContainer>
            <Box display="flex" flexWrap="wrap" gap={0.5} mt={1} justifyContent="center">
              {statusDistribution.map((d, i) => (
                <Chip key={d.name} size="small" label={`${d.name}: ${d.value}`}
                  sx={{ bgcolor: alpha(PIE_COLORS[i % PIE_COLORS.length], 0.12), fontWeight: 500, fontSize: '0.75rem' }} />
              ))}
            </Box>
          </Paper>
        </Grid>
      </Grid>

      {/* Debt List */}
      <Paper sx={{ p: 0 }}>
        <Box display="flex" justifyContent="space-between" alignItems="center" p={2}>
          <Typography variant="h6">{t.entities.customerDebt.p}</Typography>
          <TextField size="small" placeholder={t.app.search} value={search}
            onChange={(e) => setSearch(e.target.value)}
            slotProps={{ input: { startAdornment: <InputAdornment position="start"><SearchIcon fontSize="small" /></InputAdornment> } }}
            sx={{ width: 260 }}
          />
        </Box>
        <DataGrid
          rows={data?.items ?? []}
          columns={columns}
          rowCount={data?.total ?? 0}
          loading={isLoading}
          paginationMode="server"
          paginationModel={paginationModel}
          onPaginationModelChange={setPaginationModel}
          sortingMode="server"
          sortModel={sortModel}
          onSortModelChange={setSortModel}
          pageSizeOptions={[10, 25, 50]}
          disableRowSelectionOnClick
          autoHeight
          sx={{ minHeight: 350 }}
          localeText={{ noRowsLabel: t.grid.noRows }}
        />
      </Paper>

      {/* Payment Dialog */}
      <Dialog open={paymentDialog.open} onClose={() => setPaymentDialog({ open: false, debt: null })} maxWidth="xs" fullWidth>
        <DialogTitle>{t.debtManagement.recordPayment}</DialogTitle>
        <DialogContent>
          {paymentDialog.debt && (
            <Box mt={1}>
              <Typography variant="body2" color="text.secondary" mb={1}>
                חוב: {formatCurrency(paymentDialog.debt.debtAmount)} | שולם: {formatCurrency(paymentDialog.debt.paidAmount ?? 0)} | יתרה: {formatCurrency(paymentDialog.debt.remainingAmount ?? 0)}
              </Typography>
              <TextField
                fullWidth type="number" label="סכום תשלום"
                value={paymentAmount}
                onChange={(e) => setPaymentAmount(e.target.value)}
                slotProps={{ input: { inputProps: { min: 0, max: paymentDialog.debt.remainingAmount ?? 0 } } }}
              />
            </Box>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setPaymentDialog({ open: false, debt: null })}>{t.actions.cancel}</Button>
          <Button variant="contained" onClick={() => paymentMutation.mutate()}
            disabled={!paymentAmount || Number(paymentAmount) <= 0 || paymentMutation.isPending}>
            {t.actions.save}
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
