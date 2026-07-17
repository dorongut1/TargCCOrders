// DeliveryWorkflowList.tsx — Kanban-style delivery board + list view
import { useState, useMemo } from 'react';
import {
  Box, Paper, Typography, Button, Chip, Avatar, IconButton,
  Tooltip, TextField, InputAdornment, Tabs, Tab, alpha, useTheme,
  Dialog, DialogTitle, DialogContent, DialogActions, MenuItem,
} from '@mui/material';
import Grid from '@mui/material/Grid2';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { useNavigate } from 'react-router-dom';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import SearchIcon from '@mui/icons-material/Search';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import InventoryIcon from '@mui/icons-material/Inventory';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import HourglassTopIcon from '@mui/icons-material/HourglassTop';
import WarehouseIcon from '@mui/icons-material/Warehouse';
import VisibilityIcon from '@mui/icons-material/Visibility';
import EditIcon from '@mui/icons-material/Edit';
import { DeliveryApi } from '../api/DeliveryApi';
import { useEnumValues } from '../hooks/useEnumValues';
import { useNotification } from '../contexts/NotificationContext';
import useTranslation from '../i18n/useTranslation';
import type { Delivery } from '../types/Delivery';

type ViewMode = 'board' | 'list';

// Status column definitions for Kanban
const STATUS_COLUMNS = [
  { status: 1, key: 'pending', icon: <HourglassTopIcon />, color: '#D97706' },
  { status: 2, key: 'inTransit', icon: <LocalShippingIcon />, color: '#2563EB' },
  { status: 3, key: 'atHub', icon: <WarehouseIcon />, color: '#7C3AED' },
  { status: 4, key: 'delivered', icon: <CheckCircleIcon />, color: '#059669' },
];

function DeliveryCard({ delivery, onStatusChange, onView }: {
  delivery: Delivery;
  onStatusChange: (id: number, newStatus: number) => void;
  onView: (id: number) => void;
}) {
  const theme = useTheme();
  const { t } = useTranslation();

  const formatDate = (d: string | null) => d ? new Date(d).toLocaleDateString('he-IL') : '';

  return (
    <Paper
      sx={{
        p: 2, mb: 1.5, cursor: 'pointer',
        transition: 'all 0.2s',
        '&:hover': { boxShadow: 3, transform: 'translateY(-1px)' },
        borderRight: `3px solid ${STATUS_COLUMNS.find(s => s.status === delivery.enmDeliveryStatus)?.color ?? '#ccc'}`,
      }}
      onClick={() => onView(delivery.id)}
    >
      <Box display="flex" justifyContent="space-between" alignItems="start" mb={1}>
        <Typography variant="subtitle2" fontWeight={600}>
          הזמנה #{delivery.fkOrderHeaderId}
        </Typography>
        <Chip size="small" label={`#${delivery.id}`} variant="outlined" sx={{ fontSize: '0.7rem' }} />
      </Box>
      {delivery.contactName && (
        <Typography variant="body2" color="text.secondary" mb={0.5}>
          {delivery.contactName}
        </Typography>
      )}
      {delivery.deliveryAddress && (
        <Typography variant="body2" color="text.secondary" noWrap title={delivery.deliveryAddress}>
          {delivery.deliveryAddress}
        </Typography>
      )}
      <Box display="flex" gap={1} mt={1.5} flexWrap="wrap">
        {delivery.orderedDate && (
          <Chip size="small" label={`הוזמן: ${formatDate(delivery.orderedDate)}`} variant="outlined" />
        )}
        {delivery.arrivalToCustomerDate && (
          <Chip size="small" label={`נמסר: ${formatDate(delivery.arrivalToCustomerDate)}`} color="success" variant="outlined" />
        )}
      </Box>
    </Paper>
  );
}

export default function DeliveryWorkflowList() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const theme = useTheme();
  const queryClient = useQueryClient();
  const { showSuccess, showError } = useNotification();
  const [viewMode, setViewMode] = useState<ViewMode>('board');
  const [search, setSearch] = useState('');
  const [statusDialog, setStatusDialog] = useState<{ open: boolean; deliveryId: number; currentStatus: number }>({
    open: false, deliveryId: 0, currentStatus: 0,
  });
  const [newStatus, setNewStatus] = useState(0);

  const deliveryStatusOptions = useEnumValues('DeliveryStatus');
  const deliveryMethodOptions = useEnumValues('DeliveryMethod');

  const { data, isLoading } = useQuery({
    queryKey: ['deliveries', 'board', search],
    queryFn: () => DeliveryApi.getAll(0, 500, search, '', 'asc'),
    staleTime: 30_000,
  });

  // Group deliveries by status for board view
  const grouped = useMemo(() => {
    const map: Record<number, Delivery[]> = {};
    STATUS_COLUMNS.forEach(col => { map[col.status] = []; });
    (data?.items ?? []).forEach((d: Delivery) => {
      const bucket = map[d.enmDeliveryStatus] ?? map[1];
      bucket.push(d);
    });
    return map;
  }, [data]);

  const updateStatusMutation = useMutation({
    mutationFn: async ({ id, status }: { id: number; status: number }) => {
      await DeliveryApi.update(id, { enmDeliveryStatus: status } as any);
    },
    onSuccess: () => {
      showSuccess(t.messages.updated);
      queryClient.invalidateQueries({ queryKey: ['deliveries'] });
      setStatusDialog({ open: false, deliveryId: 0, currentStatus: 0 });
    },
    onError: () => showError(t.messages.error),
  });

  const handleStatusChange = (deliveryId: number, currentStatus: number) => {
    setStatusDialog({ open: true, deliveryId, currentStatus });
    setNewStatus(currentStatus);
  };

  const listColumns: GridColDef[] = [
    { field: 'id', headerName: t.fields.id, width: 70 },
    { field: 'fkOrderHeaderId', headerName: t.fields.orderNumber, width: 120,
      renderCell: (p) => <Typography fontWeight={600} color="primary">#{p.value}</Typography> },
    { field: 'contactName', headerName: t.fields.contactName, flex: 1, minWidth: 150 },
    { field: 'deliveryAddress', headerName: t.fields.deliveryAddress, flex: 1, minWidth: 200 },
    { field: 'enmDeliveryStatus', headerName: t.enums.deliveryStatus, width: 130,
      renderCell: (p) => {
        const col = STATUS_COLUMNS.find(s => s.status === p.value);
        const label = deliveryStatusOptions.find(o => o.value === p.value)?.label ?? '—';
        return <Chip size="small" label={label} sx={{ bgcolor: alpha(col?.color ?? '#888', 0.12), color: col?.color, fontWeight: 600 }} />;
      },
    },
    { field: 'orderedDate', headerName: t.fields.orderedDate, width: 120,
      valueFormatter: (v: string) => v ? new Date(v).toLocaleDateString('he-IL') : '' },
    { field: 'arrivalToCustomerDate', headerName: t.fields.arrivalToCustomerDate, width: 120,
      valueFormatter: (v: string) => v ? new Date(v).toLocaleDateString('he-IL') : '' },
    { field: 'location', headerName: t.fields.location, width: 130 },
    { field: 'actions', headerName: '', width: 80, sortable: false,
      renderCell: (p) => (
        <Box display="flex" gap={0.5}>
          <IconButton size="small" onClick={() => navigate(`/deliveries/${p.row.id}`)}>
            <VisibilityIcon fontSize="small" />
          </IconButton>
          <IconButton size="small" onClick={() => handleStatusChange(p.row.id, p.row.enmDeliveryStatus)}>
            <EditIcon fontSize="small" />
          </IconButton>
        </Box>
      ),
    },
  ];

  return (
    <Box>
      {/* Header */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Box>
          <Typography variant="h4">{t.deliveryWorkflow.title}</Typography>
          <Typography variant="body2" color="text.secondary">
            {data?.total ?? 0} {t.entities.delivery.p}
          </Typography>
        </Box>
        <Box display="flex" gap={1}>
          <TextField
            size="small" placeholder={t.app.search} value={search}
            onChange={(e) => setSearch(e.target.value)}
            slotProps={{ input: { startAdornment: <InputAdornment position="start"><SearchIcon fontSize="small" /></InputAdornment> } }}
            sx={{ width: 250 }}
          />
          <Tabs value={viewMode} onChange={(_, v) => setViewMode(v)} sx={{ minHeight: 36 }}>
            <Tab label={t.deliveryWorkflow.board} value="board" sx={{ minHeight: 36, py: 0.5 }} />
            <Tab label="רשימה" value="list" sx={{ minHeight: 36, py: 0.5 }} />
          </Tabs>
        </Box>
      </Box>

      {/* Board View */}
      {viewMode === 'board' && (
        <Grid container spacing={2}>
          {STATUS_COLUMNS.map(col => {
            const items = grouped[col.status] ?? [];
            const label = deliveryStatusOptions.find(o => o.value === col.status)?.label
              ?? (t.deliveryWorkflow as Record<string, string>)[col.key] ?? col.key;

            return (
              <Grid key={col.status} size={{ xs: 12, sm: 6, md: 3 }}>
                <Paper sx={{
                  p: 2, minHeight: 400,
                  background: alpha(col.color, theme.palette.mode === 'dark' ? 0.05 : 0.03),
                  border: `1px solid ${alpha(col.color, 0.2)}`,
                }}>
                  <Box display="flex" alignItems="center" gap={1} mb={2}>
                    <Avatar sx={{ bgcolor: alpha(col.color, 0.15), color: col.color, width: 32, height: 32 }}>
                      {col.icon}
                    </Avatar>
                    <Typography variant="subtitle1" fontWeight={600}>{label}</Typography>
                    <Chip size="small" label={items.length} sx={{ ml: 'auto', fontWeight: 700 }} />
                  </Box>
                  {items.length === 0 ? (
                    <Typography color="text.secondary" variant="body2" textAlign="center" py={4}>
                      {t.messages.noData}
                    </Typography>
                  ) : (
                    items.map(delivery => (
                      <DeliveryCard
                        key={delivery.id}
                        delivery={delivery}
                        onStatusChange={handleStatusChange}
                        onView={(id) => navigate(`/deliveries/${id}`)}
                      />
                    ))
                  )}
                </Paper>
              </Grid>
            );
          })}
        </Grid>
      )}

      {/* List View */}
      {viewMode === 'list' && (
        <Paper>
          <DataGrid
            rows={data?.items ?? []}
            columns={listColumns}
            rowCount={data?.total ?? 0}
            loading={isLoading}
            disableRowSelectionOnClick
            autoHeight
            pageSizeOptions={[25, 50, 100]}
            sx={{ minHeight: 400 }}
            localeText={{ noRowsLabel: t.grid.noRows }}
          />
        </Paper>
      )}

      {/* Status Change Dialog */}
      <Dialog open={statusDialog.open} onClose={() => setStatusDialog(s => ({ ...s, open: false }))} maxWidth="xs" fullWidth>
        <DialogTitle>{t.deliveryWorkflow.updateStatus}</DialogTitle>
        <DialogContent>
          <TextField
            select fullWidth value={newStatus}
            onChange={(e) => setNewStatus(Number(e.target.value))}
            sx={{ mt: 1 }}
          >
            {deliveryStatusOptions.map(opt => (
              <MenuItem key={opt.value} value={opt.value}>{opt.label}</MenuItem>
            ))}
          </TextField>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setStatusDialog(s => ({ ...s, open: false }))}>{t.actions.cancel}</Button>
          <Button
            variant="contained"
            onClick={() => updateStatusMutation.mutate({ id: statusDialog.deliveryId, status: newStatus })}
            disabled={updateStatusMutation.isPending}
          >
            {t.actions.save}
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
