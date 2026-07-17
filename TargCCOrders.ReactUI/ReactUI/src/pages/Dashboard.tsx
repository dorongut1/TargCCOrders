// Dashboard.tsx — Professional management dashboard
// KPI cards, revenue chart, recent orders, delivery status, debt alerts
import { useMemo } from 'react';
import { useQuery } from '@tanstack/react-query';
import {
  Box, Paper, Typography, Skeleton, Button, Chip, Avatar, IconButton, Tooltip, alpha, useTheme,
} from '@mui/material';
import Grid from '@mui/material/Grid2';
import {
  BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip as RTooltip,
  ResponsiveContainer, PieChart, Pie, Cell, LineChart, Line, Area, AreaChart,
} from 'recharts';
import { useNavigate } from 'react-router-dom';
import TrendingUpIcon from '@mui/icons-material/TrendingUp';
import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';
import AddIcon from '@mui/icons-material/Add';
import ReceiptIcon from '@mui/icons-material/Receipt';
import FiberManualRecordIcon from '@mui/icons-material/FiberManualRecord';
import { api } from '../api/client';
import { useDashboardSummary } from '../hooks/useDashboardSummary';
import useTranslation from '../i18n/useTranslation';

// ── Hooks ──
function useHealthCheck() {
  return useQuery({
    queryKey: ['health'],
    queryFn: async () => {
      try {
        const { data } = await api.get<{ status: string; timestamp: string; version: string }>('/health');
        return data;
      } catch { return { status: 'error', timestamp: new Date().toISOString(), version: '' }; }
    },
    refetchInterval: 60000,
    retry: false,
  });
}

// Hebrew month labels for 'yyyy-MM' keys
const HEBREW_MONTHS = ['ינו', 'פבר', 'מרץ', 'אפר', 'מאי', 'יונ', 'יול', 'אוג', 'ספט', 'אוק', 'נוב', 'דצמ'];
function hebrewMonthLabel(yyyyMM: string): string {
  const [year, month] = yyyyMM.split('-');
  const idx = Number(month) - 1;
  const label = HEBREW_MONTHS[idx] ?? yyyyMM;
  return year ? `${label} ${year.slice(2)}` : label;
}

// Hebrew labels for order status codes from the summary endpoint
const ORDER_STATUS_LABELS: Record<string, string> = {
  New: 'חדשה',
  InProgress: 'בטיפול',
  Processing: 'בעיבוד',
  Shipped: 'נשלחה',
  Completed: 'הושלמה',
  Cancelled: 'בוטלה',
  UD: 'לא מוגדר',
};

// ── KPI Card Component ──
function KpiCard({ title, value, subtitle, icon, color, onClick, loading }: {
  title: string; value: string | number; subtitle?: string;
  icon: React.ReactNode; color: string; onClick?: () => void; loading?: boolean;
}) {
  const theme = useTheme();
  return (
    <Paper
      sx={{
        p: 3, cursor: onClick ? 'pointer' : 'default',
        transition: 'all 0.2s',
        '&:hover': onClick ? { transform: 'translateY(-3px)', boxShadow: `0 8px 25px ${alpha(color, 0.15)}` } : {},
        position: 'relative', overflow: 'hidden',
      }}
      onClick={onClick}
    >
      <Box sx={{
        position: 'absolute', top: -20, left: -20, width: 100, height: 100,
        borderRadius: '50%', background: alpha(color, 0.08),
      }} />
      <Box display="flex" justifyContent="space-between" alignItems="flex-start">
        <Box>
          <Typography variant="body2" color="text.secondary" fontWeight={500}>{title}</Typography>
          {loading ? (
            <Skeleton width={80} height={42} />
          ) : (
            <Typography variant="h4" fontWeight={700} mt={0.5}>{value}</Typography>
          )}
          {subtitle && <Typography variant="caption" color="text.secondary">{subtitle}</Typography>}
        </Box>
        <Avatar sx={{ bgcolor: alpha(color, 0.12), color, width: 48, height: 48 }}>
          {icon}
        </Avatar>
      </Box>
    </Paper>
  );
}

// ── Pie Chart Colors ──
const PIE_COLORS = ['#2563EB', '#7C3AED', '#059669', '#D97706', '#DC2626', '#0891B2'];

export default function Dashboard() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const theme = useTheme();
  const { data: health } = useHealthCheck();

  // Real dashboard data from /api/dashboard/summary
  const { data: summary, isLoading: summaryLoading } = useDashboardSummary();

  const formatCurrency = (n: number | undefined) =>
    n != null ? `₪${n.toLocaleString('he-IL', { maximumFractionDigits: 0 })}` : '—';

  // Monthly series with Hebrew month labels
  const monthlyData = useMemo(() =>
    (summary?.monthlySeries ?? []).map(p => ({
      month: hebrewMonthLabel(p.month),
      orders: p.orders,
      revenue: p.revenue,
    })), [summary]);

  // Orders-by-status distribution with Hebrew labels
  const statusChartData = useMemo(() =>
    (summary?.ordersByStatus ?? [])
      .filter(s => s.count > 0)
      .map(s => ({ name: ORDER_STATUS_LABELS[s.status] ?? s.status, count: s.count })),
    [summary]);

  return (
    <Box>
      {/* Header */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Box>
          <Typography variant="h4">{t.dashboard.title}</Typography>
          <Box display="flex" alignItems="center" gap={0.5} mt={0.5}>
            <FiberManualRecordIcon
              sx={{ fontSize: 10, color: health?.status === 'ok' ? 'success.main' : 'error.main' }}
            />
            <Typography variant="caption" color="text.secondary">
              {health?.status === 'ok' ? t.app.apiOnline : t.app.apiOffline}
              {health?.version ? ` v${health.version}` : ''}
            </Typography>
          </Box>
        </Box>
        <Box display="flex" gap={1}>
          <Button variant="outlined" startIcon={<AddIcon />} onClick={() => navigate('/customers/new')}>
            {t.dashboard.newCustomer}
          </Button>
          <Button variant="contained" startIcon={<AddIcon />} onClick={() => navigate('/orders/new')}>
            {t.dashboard.newOrder}
          </Button>
        </Box>
      </Box>

      {/* KPI Cards */}
      <Grid container spacing={2.5} mb={3}>
        <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
          <KpiCard
            title={t.dashboard.monthRevenue}
            value={formatCurrency(summary?.monthRevenue)}
            subtitle={summary ? `${summary.monthOrders.toLocaleString('he-IL')} ${t.entities.orderHeader.p}` : undefined}
            icon={<TrendingUpIcon />}
            color="#2563EB"
            loading={summaryLoading}
            onClick={() => navigate('/orders')}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
          <KpiCard
            title={t.dashboard.openOrders}
            value={summary?.openOrders.toLocaleString('he-IL') ?? '—'}
            icon={<ReceiptIcon />}
            color="#7C3AED"
            loading={summaryLoading}
            onClick={() => navigate('/orders')}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
          <KpiCard
            title={t.dashboard.pendingDeliveries}
            value={summary?.pendingDeliveries.toLocaleString('he-IL') ?? '—'}
            icon={<LocalShippingIcon />}
            color="#059669"
            loading={summaryLoading}
            onClick={() => navigate('/delivery-board')}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
          <KpiCard
            title={t.dashboard.debtsNeedingAttention}
            value={summary?.debtsNeedingAttention.toLocaleString('he-IL') ?? '—'}
            subtitle={summary ? `${t.dashboard.openDebtTotal}: ${formatCurrency(summary.openDebtTotal)}` : undefined}
            icon={<WarningAmberIcon />}
            color="#DC2626"
            loading={summaryLoading}
            onClick={() => navigate('/debt-management')}
          />
        </Grid>
      </Grid>

      {/* Charts Row */}
      <Grid container spacing={2.5} mb={3}>
        {/* Revenue Trend */}
        <Grid size={{ xs: 12, lg: 8 }}>
          <Paper sx={{ p: 3 }}>
            <Typography variant="h6" mb={2}>{t.dashboard.monthlyRevenue}</Typography>
            <ResponsiveContainer width="100%" height={300}>
              <AreaChart data={monthlyData} margin={{ top: 5, right: 20, left: 20, bottom: 5 }}>
                <defs>
                  <linearGradient id="colorRevenue" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="5%" stopColor="#2563EB" stopOpacity={0.2}/>
                    <stop offset="95%" stopColor="#2563EB" stopOpacity={0}/>
                  </linearGradient>
                </defs>
                <CartesianGrid strokeDasharray="3 3" stroke={theme.palette.divider} />
                <XAxis dataKey="month" stroke={theme.palette.text.secondary} fontSize={12} />
                <YAxis stroke={theme.palette.text.secondary} fontSize={12}
                  tickFormatter={(v) => `₪${(v/1000).toFixed(0)}K`} />
                <RTooltip
                  contentStyle={{
                    backgroundColor: theme.palette.background.paper,
                    border: `1px solid ${theme.palette.divider}`,
                    borderRadius: 8,
                    direction: 'rtl',
                  }}
                  formatter={(value: number) => [`₪${value.toLocaleString('he-IL')}`, t.dashboard.monthlyRevenue]}
                />
                <Area type="monotone" dataKey="revenue" stroke="#2563EB" strokeWidth={2.5}
                  fill="url(#colorRevenue)" />
              </AreaChart>
            </ResponsiveContainer>
          </Paper>
        </Grid>

        {/* Orders by Status */}
        <Grid size={{ xs: 12, lg: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" mb={2}>{t.dashboard.ordersByStatus}</Typography>
            {statusChartData.length > 0 ? (
              <ResponsiveContainer width="100%" height={250}>
                <PieChart>
                  <Pie
                    data={statusChartData}
                    cx="50%" cy="50%"
                    innerRadius={55} outerRadius={90}
                    paddingAngle={3}
                    dataKey="count"
                  >
                    {statusChartData.map((_, i) => (
                      <Cell key={i} fill={PIE_COLORS[i % PIE_COLORS.length]} />
                    ))}
                  </Pie>
                  <RTooltip
                    contentStyle={{
                      backgroundColor: theme.palette.background.paper,
                      border: `1px solid ${theme.palette.divider}`,
                      borderRadius: 8,
                      direction: 'rtl',
                    }}
                  />
                </PieChart>
              </ResponsiveContainer>
            ) : (
              <Box display="flex" justifyContent="center" alignItems="center" height={250}>
                <Typography color="text.secondary">{t.messages.loading}</Typography>
              </Box>
            )}
            {/* Legend */}
            <Box display="flex" flexWrap="wrap" gap={1} mt={1}>
              {statusChartData.map((d, i) => (
                <Chip
                  key={d.name}
                  size="small"
                  label={`${d.name}: ${d.count}`}
                  sx={{ bgcolor: alpha(PIE_COLORS[i % PIE_COLORS.length], 0.12), fontWeight: 500 }}
                />
              ))}
            </Box>
          </Paper>
        </Grid>
      </Grid>

      {/* Orders Trend Bar Chart */}
      <Grid container spacing={2.5}>
        <Grid size={{ xs: 12, lg: 6 }}>
          <Paper sx={{ p: 3 }}>
            <Typography variant="h6" mb={2}>{t.dashboard.ordersByMonth}</Typography>
            <ResponsiveContainer width="100%" height={280}>
              <BarChart data={monthlyData} margin={{ top: 5, right: 20, left: 10, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" stroke={theme.palette.divider} />
                <XAxis dataKey="month" stroke={theme.palette.text.secondary} fontSize={12} />
                <YAxis stroke={theme.palette.text.secondary} fontSize={12} />
                <RTooltip
                  contentStyle={{
                    backgroundColor: theme.palette.background.paper,
                    border: `1px solid ${theme.palette.divider}`,
                    borderRadius: 8,
                    direction: 'rtl',
                  }}
                />
                <Bar dataKey="orders" fill="#7C3AED" radius={[4, 4, 0, 0]} name={t.entities.orderHeader.p} />
              </BarChart>
            </ResponsiveContainer>
          </Paper>
        </Grid>

        {/* Quick Actions */}
        <Grid size={{ xs: 12, lg: 6 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" mb={2}>{t.dashboard.quickActions}</Typography>
            <Grid container spacing={1.5}>
              {[
                { label: t.dashboard.newOrder, path: '/orders/new', color: '#2563EB', icon: <ReceiptIcon /> },
                { label: t.dashboard.newCustomer, path: '/customers/new', color: '#7C3AED', icon: <PeopleIcon /> },
                { label: t.deliveryWorkflow.title, path: '/delivery-board', color: '#059669', icon: <LocalShippingIcon /> },
                { label: t.debtManagement.title, path: '/debt-management', color: '#DC2626', icon: <WarningAmberIcon /> },
              ].map(action => (
                <Grid key={action.path} size={{ xs: 6 }}>
                  <Paper
                    sx={{
                      p: 2.5, textAlign: 'center', cursor: 'pointer',
                      transition: 'all 0.2s',
                      '&:hover': { transform: 'translateY(-2px)', boxShadow: `0 4px 12px ${alpha(action.color, 0.2)}` },
                      border: `1px solid ${alpha(action.color, 0.2)}`,
                    }}
                    onClick={() => navigate(action.path)}
                  >
                    <Avatar sx={{ bgcolor: alpha(action.color, 0.12), color: action.color, mx: 'auto', mb: 1, width: 44, height: 44 }}>
                      {action.icon}
                    </Avatar>
                    <Typography variant="body2" fontWeight={600}>{action.label}</Typography>
                  </Paper>
                </Grid>
              ))}
            </Grid>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
