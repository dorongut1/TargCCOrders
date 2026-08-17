// OrderCompositeList.tsx — Composite order management list
// Shows orders with inline line counts, customer name, totals
import { useState, useMemo } from 'react';
import {
  Box, Paper, Typography, Button, Chip, TextField, InputAdornment,
  IconButton, Tooltip,
} from '@mui/material';
import { DataGrid, type GridColDef, type GridPaginationModel, type GridSortModel } from '@mui/x-data-grid';
import { useNavigate } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import AddIcon from '@mui/icons-material/Add';
import SearchIcon from '@mui/icons-material/Search';
import FilterListIcon from '@mui/icons-material/FilterList';
import VisibilityIcon from '@mui/icons-material/Visibility';
import EditIcon from '@mui/icons-material/Edit';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import { OrderHeaderApi } from '../api/OrderHeaderApi';
import { useEnumValues, useEnumLabel } from '../hooks/useEnumValues';
import useTranslation from '../i18n/useTranslation';
import { useEntityLookup } from '../hooks/useEntityLookup';
import type { OrderHeader } from '../types/OrderHeader';

export default function OrderCompositeList() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const customerName = useEntityLookup('customer');
  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({ page: 0, pageSize: 25 });
  const [sortModel, setSortModel] = useState<GridSortModel>([{ field: 'orderDate', sort: 'desc' }]);
  const [search, setSearch] = useState('');

  const orderStatusOptions = useEnumValues('OrderStatus');
  const paymentStatusOptions = useEnumValues('PaymentStatus');
  const deliveryMethodOptions = useEnumValues('DeliveryMethod');

  const filters = useMemo(() => {
    const f: Record<string, unknown> = {};
    return f;
  }, []);

  const { data, isLoading } = useQuery({
    queryKey: ['orderHeaders', 'composite', paginationModel, sortModel, search, filters],
    queryFn: () => OrderHeaderApi.getAll(
      paginationModel.page,
      paginationModel.pageSize,
      search,
      sortModel[0]?.field ?? '',
      sortModel[0]?.sort ?? 'asc',
      filters,
    ),
    staleTime: 30_000,
  });

  const statusColor = (status: number | null): 'default' | 'primary' | 'success' | 'warning' | 'error' => {
    switch (status) {
      case 1: return 'primary';    // New/Open
      case 2: return 'warning';    // In Progress
      case 3: return 'success';    // Completed
      case 4: return 'error';      // Cancelled
      default: return 'default';
    }
  };

  const paymentColor = (status: number | null): 'default' | 'success' | 'warning' | 'error' => {
    switch (status) {
      case 1: return 'warning';    // Pending
      case 2: return 'success';    // Paid
      case 3: return 'error';      // Overdue
      default: return 'default';
    }
  };

  const columns: GridColDef[] = [
    {
      field: 'orderNumber',
      headerName: t.fields.orderNumber,
      width: 120,
      renderCell: (params) => (
        <Typography fontWeight={600} color="primary" sx={{ cursor: 'pointer' }}
          onClick={() => navigate(`/orders/${params.row.id}`)}>
          #{params.value}
        </Typography>
      ),
    },
    {
      field: 'fkCustomerId',
      headerName: t.fields.customerName,
      flex: 1,
      minWidth: 180,
      // The server declares customerDisplayName but never fills it, so every
      // row fell through to "לקוח #5". Resolve it from the customer list
      // instead; the server value still wins if it ever starts arriving.
      renderCell: (params) => (
        <Typography variant="body2">
          {params.row.customerDisplayName || customerName(params.value as number)}
        </Typography>
      ),
    },
    {
      field: 'orderDate',
      headerName: t.fields.orderDate,
      width: 130,
      valueFormatter: (value: string) => value ? new Date(value).toLocaleDateString('he-IL') : '',
    },
    {
      field: 'totalWithVat',
      headerName: t.fields.totalWithVat,
      width: 140,
      align: 'left',
      headerAlign: 'left',
      renderCell: (params) => (
        <Typography fontWeight={600} dir="ltr">
          {params.value != null ? `₪${Number(params.value).toLocaleString('he-IL', { minimumFractionDigits: 2 })}` : '—'}
        </Typography>
      ),
    },
    {
      field: 'enmOrderStatus',
      headerName: t.enums.orderStatus,
      width: 130,
      renderCell: (params) => {
        const label = orderStatusOptions.find(o => o.value === params.value)?.label ?? '';
        return <Chip label={label || '—'} size="small" color={statusColor(params.value)} variant="outlined" />;
      },
    },
    {
      field: 'enmPaymentStatus',
      headerName: t.enums.paymentStatus,
      width: 130,
      renderCell: (params) => {
        const label = paymentStatusOptions.find(o => o.value === params.value)?.label ?? '';
        return <Chip label={label || '—'} size="small" color={paymentColor(params.value)} />;
      },
    },
    {
      field: 'enmDeliveryMethod',
      headerName: t.enums.deliveryMethod,
      width: 120,
      renderCell: (params) => {
        const label = deliveryMethodOptions.find(o => o.value === params.value)?.label ?? '';
        return <Typography variant="body2">{label || '—'}</Typography>;
      },
    },
    {
      field: 'actions',
      headerName: '',
      width: 100,
      sortable: false,
      filterable: false,
      renderCell: (params) => (
        <Box display="flex" gap={0.5}>
          <Tooltip title={t.actions.view}>
            <IconButton size="small" onClick={() => navigate(`/orders/${params.row.id}`)}>
              <VisibilityIcon fontSize="small" />
            </IconButton>
          </Tooltip>
          <Tooltip title={t.actions.edit}>
            <IconButton size="small" onClick={() => navigate(`/orders/${params.row.id}/edit`)}>
              <EditIcon fontSize="small" />
            </IconButton>
          </Tooltip>
        </Box>
      ),
    },
  ];

  return (
    <Box>
      {/* Header */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Box>
          <Typography variant="h4">{t.entities.orderHeader.p}</Typography>
          <Typography variant="body2" color="text.secondary">
            {data?.total ?? 0} {t.entities.orderHeader.p}
          </Typography>
        </Box>
        <Button
          variant="contained" startIcon={<AddIcon />}
          onClick={() => navigate('/orders/new')}
          sx={{ px: 3, py: 1.2 }}
        >
          {t.orderComposite.newOrder}
        </Button>
      </Box>

      {/* Search */}
      {/* TODO: restore status tabs once the API supports server-side tab filtering — the previous tabs did not filter anything */}
      <Paper sx={{ mb: 2, p: 0 }}>
        <Box display="flex" justifyContent="flex-end" alignItems="center" px={2} py={1}>
          <TextField
            size="small"
            placeholder={t.app.search}
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            slotProps={{
              input: {
                startAdornment: <InputAdornment position="start"><SearchIcon fontSize="small" /></InputAdornment>,
              },
            }}
            sx={{ width: 280 }}
          />
        </Box>
      </Paper>

      {/* Data Grid */}
      <Paper>
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
          pageSizeOptions={[10, 25, 50, 100]}
          disableRowSelectionOnClick
          onRowDoubleClick={(params) => navigate(`/orders/${params.id}`)}
          autoHeight
          sx={{
            '& .MuiDataGrid-row': { cursor: 'pointer' },
            minHeight: 400,
          }}
          localeText={{
            noRowsLabel: t.grid.noRows,
            MuiTablePagination: {
              labelRowsPerPage: t.grid.rowsPerPage,
            },
          }}
        />
      </Paper>
    </Box>
  );
}
