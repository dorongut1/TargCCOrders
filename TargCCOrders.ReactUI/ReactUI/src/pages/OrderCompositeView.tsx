// OrderCompositeView.tsx — Read-only order detail with lines
import { Box, Paper, Typography, Button, Chip, Divider, Skeleton, IconButton, Tooltip } from '@mui/material';
import Grid from '@mui/material/Grid2';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { useNavigate, useParams } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import EditIcon from '@mui/icons-material/Edit';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import PrintIcon from '@mui/icons-material/Print';
import { OrderCompositeApi } from '../api/OrderCompositeApi';
import { CustomerApi } from '../api/CustomerApi';
import { useEnumValues } from '../hooks/useEnumValues';
import useTranslation from '../i18n/useTranslation';

function InfoItem({ label, value, chip, chipColor }: {
  label: string; value: string | number | null; chip?: boolean;
  chipColor?: 'default' | 'primary' | 'success' | 'warning' | 'error';
}) {
  return (
    <Box mb={1.5}>
      <Typography variant="caption" color="text.secondary" display="block">{label}</Typography>
      {chip ? (
        <Chip label={value ?? '—'} size="small" color={chipColor ?? 'default'} variant="outlined" />
      ) : (
        <Typography variant="body1" fontWeight={500}>{value ?? '—'}</Typography>
      )}
    </Box>
  );
}

export default function OrderCompositeView() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const parsedId = Number(id);

  // Single composite fetch: header (with customerDisplayName) + lines (with productDisplayName)
  const { data: composite, isLoading } = useQuery({
    queryKey: ['orders', 'composite', parsedId],
    queryFn: () => OrderCompositeApi.getComposite(parsedId),
    enabled: parsedId > 0,
  });
  const order = composite?.header;
  const lines = composite?.lines ?? [];

  const { data: customer } = useQuery({
    queryKey: ['customers', order?.fkCustomerId],
    queryFn: () => CustomerApi.getById(order!.fkCustomerId),
    enabled: Boolean(order?.fkCustomerId),
  });

  const orderStatusOpts = useEnumValues('OrderStatus');
  const paymentStatusOpts = useEnumValues('PaymentStatus');
  const paymentMethodOpts = useEnumValues('PaymentMethod');
  const deliveryMethodOpts = useEnumValues('DeliveryMethod');

  const enumLabel = (opts: { value: number; label: string }[], val: number | null) =>
    opts.find(o => o.value === val)?.label ?? '—';

  const lineColumns: GridColDef[] = [
    { field: 'lineNumber', headerName: '#', width: 60 },
    {
      field: 'productDisplayName', headerName: t.fields.productName, flex: 1, minWidth: 180,
      renderCell: (p) => p.value || `מוצר #${p.row.fkProductId}`,
    },
    { field: 'quantity', headerName: t.fields.quantity, width: 90 },
    {
      field: 'unitPrice', headerName: t.fields.unitPrice, width: 120,
      renderCell: (p) => <span dir="ltr">₪{Number(p.value).toFixed(2)}</span>,
    },
    { field: 'discountPercent', headerName: t.fields.discountPercent, width: 100,
      renderCell: (p) => p.value ? `${p.value}%` : '—',
    },
    {
      field: 'lineTotal', headerName: t.fields.lineTotal, width: 130,
      renderCell: (p) => (
        <Typography fontWeight={600} dir="ltr">₪{Number(p.value ?? 0).toFixed(2)}</Typography>
      ),
    },
  ];

  if (isLoading) {
    return (
      <Box p={3}>
        <Skeleton height={60} sx={{ mb: 2 }} />
        <Skeleton height={250} sx={{ mb: 2 }} />
        <Skeleton height={300} />
      </Box>
    );
  }

  if (!order) {
    return (
      <Box p={4} textAlign="center">
        <Typography variant="h5" color="error">{t.messages.notFound}</Typography>
        <Button sx={{ mt: 2 }} onClick={() => navigate('/orders')}>{t.actions.back}</Button>
      </Box>
    );
  }

  const formatDate = (d: string | null) => d ? new Date(d).toLocaleDateString('he-IL') : '—';
  const formatCurrency = (n: number | null) => n != null ? `₪${n.toLocaleString('he-IL', { minimumFractionDigits: 2 })}` : '—';

  return (
    <Box>
      {/* Header */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Box display="flex" alignItems="center" gap={2}>
          <IconButton onClick={() => navigate('/orders')}><ArrowForwardIcon /></IconButton>
          <Box>
            <Typography variant="h4">
              {t.orderComposite.title} #{order.orderNumber}
            </Typography>
            <Box display="flex" gap={1} mt={0.5}>
              <Chip size="small" label={enumLabel(orderStatusOpts, order.enmOrderStatus)} color="primary" variant="outlined" />
              <Chip size="small" label={enumLabel(paymentStatusOpts, order.enmPaymentStatus)}
                color={order.enmPaymentStatus === 2 ? 'success' : 'warning'} />
            </Box>
          </Box>
        </Box>
        <Box display="flex" gap={1}>
          <Button variant="outlined" startIcon={<PrintIcon />} onClick={() => window.print()}>{t.actions.print}</Button>
          <Button variant="contained" startIcon={<EditIcon />}
            onClick={() => navigate(`/orders/${parsedId}/edit`)}>
            {t.actions.edit}
          </Button>
        </Box>
      </Box>

      {/* Order + Customer Info */}
      <Grid container spacing={3} mb={3}>
        <Grid size={{ xs: 12, md: 6 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" mb={2}>{t.orderComposite.orderDetails}</Typography>
            <Grid container spacing={2}>
              <Grid size={6}><InfoItem label={t.fields.orderDate} value={formatDate(order.orderDate)} /></Grid>
              <Grid size={6}><InfoItem label={t.fields.invoiceNumber} value={order.invoiceNumber} /></Grid>
              <Grid size={6}><InfoItem label={t.enums.paymentMethod} value={enumLabel(paymentMethodOpts, order.enmPaymentMethod)} /></Grid>
              <Grid size={6}><InfoItem label={t.enums.deliveryMethod} value={enumLabel(deliveryMethodOpts, order.enmDeliveryMethod)} /></Grid>
              <Grid size={6}><InfoItem label={t.fields.deliveryDate} value={formatDate(order.deliveryDate)} /></Grid>
              <Grid size={6}><InfoItem label={t.fields.orderMonth} value={order.orderMonth} /></Grid>
            </Grid>
            {order.notes && (
              <Box mt={2}>
                <Typography variant="caption" color="text.secondary">{t.fields.notes}</Typography>
                <Typography>{order.notes}</Typography>
              </Box>
            )}
          </Paper>
        </Grid>

        <Grid size={{ xs: 12, md: 3 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" mb={2}>{t.fields.customerName}</Typography>
            {customer ? (
              <>
                <Typography variant="h5" fontWeight={600}>{customer.customerName}</Typography>
                <Typography color="text.secondary" mb={2}>{customer.customerCode}</Typography>
                <InfoItem label={t.fields.phone} value={customer.phone} />
                <InfoItem label={t.fields.email} value={customer.email} />
                <InfoItem label={t.fields.city} value={customer.city} />
              </>
            ) : (
              <Typography color="text.secondary">
                {order.customerDisplayName || `לקוח #${order.fkCustomerId}`}
              </Typography>
            )}
          </Paper>
        </Grid>

        <Grid size={{ xs: 12, md: 3 }}>
          <Paper sx={{
            p: 3, height: '100%',
            background: (theme) => theme.palette.mode === 'dark'
              ? 'linear-gradient(135deg, #1E293B 0%, #0F172A 100%)'
              : 'linear-gradient(135deg, #EFF6FF 0%, #DBEAFE 100%)',
          }}>
            <Typography variant="h6" mb={2}>{t.orderComposite.summary}</Typography>
            <InfoItem label={t.orderComposite.subtotal} value={formatCurrency(order.totalAmount)} />
            <InfoItem label={t.orderComposite.vat} value={formatCurrency(order.vatAmount)} />
            <Divider sx={{ my: 1.5 }} />
            <Typography variant="caption" color="text.secondary">{t.orderComposite.grandTotal}</Typography>
            <Typography variant="h4" fontWeight={700} color="primary" dir="ltr">
              {formatCurrency(order.totalWithVat)}
            </Typography>
          </Paper>
        </Grid>
      </Grid>

      {/* Order Lines */}
      <Paper sx={{ p: 3 }}>
        <Typography variant="h6" mb={2}>
          {t.orderComposite.orderLines} ({lines.length})
        </Typography>
        <DataGrid
          rows={lines}
          columns={lineColumns}
          autoHeight
          hideFooter={lines.length <= 25}
          disableRowSelectionOnClick
          sx={{ minHeight: 200 }}
          localeText={{ noRowsLabel: t.orderComposite.noLines }}
        />
      </Paper>
    </Box>
  );
}
