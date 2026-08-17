// OrderCompositeForm.tsx — Composite Order Screen
// Single page: Header details + editable order lines table + live totals
import { useEffect, useState, useMemo, useCallback, useRef } from 'react';
import { useForm, Controller, useFieldArray } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import {
  Box, Paper, Typography, Button, TextField, MenuItem, Autocomplete,
  Divider, IconButton, Tooltip, Alert, Dialog, DialogTitle,
  DialogContent, DialogActions, Chip, Skeleton, Stack,
} from '@mui/material';
import Grid from '@mui/material/Grid2';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { useNavigate, useParams } from 'react-router-dom';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ReceiptIcon from '@mui/icons-material/Receipt';
import { OrderHeaderApi } from '../api/OrderHeaderApi';
import { OrderLineApi } from '../api/OrderLineApi';
import { CustomerApi } from '../api/CustomerApi';
import { ProductApi } from '../api/ProductApi';
import { PricingApi } from '../api/PricingApi';
import { OrderCompositeApi, type CompositeLinePayload } from '../api/OrderCompositeApi';
import { useBusinessSettings } from '../hooks/useBusinessSettings';
import { useEnumValues, useEnumValueByName } from '../hooks/useEnumValues';
import { useNotification } from '../contexts/NotificationContext';
import useTranslation from '../i18n/useTranslation';
import type { OrderHeader } from '../types/OrderHeader';
import type { OrderLine } from '../types/OrderLine';
import type { Product } from '../types/Product';
import type { Customer } from '../types/Customer';

// ── Schema ──
const orderSchema = z.object({
  orderNumber: z.coerce.number().min(1, 'מספר הזמנה נדרש'),
  fkCustomerId: z.coerce.number().min(1, 'יש לבחור לקוח'),
  orderDate: z.string().min(1, 'תאריך הזמנה נדרש'),
  enmPaymentMethod: z.coerce.number().optional().nullable(),
  enmPaymentStatus: z.coerce.number().optional().nullable(),
  paymentDate: z.string().optional().nullable(),
  invoiceNumber: z.string().max(50).optional().nullable(),
  enmDeliveryMethod: z.coerce.number().optional().nullable(),
  deliveryDate: z.string().optional().nullable(),
  enmDeliveryDay: z.coerce.number().optional().nullable(),
  enmOrderStatus: z.coerce.number().optional().nullable(),
  notes: z.string().optional().nullable(),
  notes2: z.string().optional().nullable(),
});
type OrderFormData = z.infer<typeof orderSchema>;

interface LineRow {
  id: number;
  tempId?: string;
  fkProductId: number;
  productName?: string;
  quantity: number;
  unitPrice: number;
  discountPercent: number;
  lineNumber: number;
  lineTotal: number;
  isNew?: boolean;
  isDeleted?: boolean;
  isDirty?: boolean;
}

/** Round money values to 2 decimals */
const round2 = (x: number) => Math.round(x * 100) / 100;

/** Local-time datetime string for <input type="datetime-local"> (avoids UTC shift of toISOString) */
function nowLocalDateTime(): string {
  const d = new Date();
  const pad = (n: number) => String(n).padStart(2, '0');
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`;
}

export default function OrderCompositeForm() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const isEditMode = Boolean(id);
  const parsedId = id ? Number(id) : 0;
  const queryClient = useQueryClient();
  const { showSuccess, showError, showWarning } = useNotification();
  const { settings } = useBusinessSettings();
  const vatRate = settings.vatRatePercent;

  // ── Data fetching ──
  const { data: orderData, isLoading: orderLoading } = useQuery({
    queryKey: ['orderHeaders', parsedId],
    queryFn: () => OrderHeaderApi.getById(parsedId),
    enabled: isEditMode,
  });

  const { data: orderLines, isLoading: linesLoading } = useQuery({
    queryKey: ['orderLines', 'byOrder', parsedId],
    queryFn: () => OrderLineApi.getAll(0, 999, '', '', 'asc', { fkOrderHeaderId: parsedId }),
    enabled: isEditMode,
  });

  const { data: customerData } = useQuery({
    queryKey: ['customers', 'all'],
    queryFn: () => CustomerApi.getAll(0, 9999, ''),
    staleTime: 5 * 60_000,
  });

  const { data: productData } = useQuery({
    queryKey: ['products', 'all'],
    queryFn: () => ProductApi.getAll(0, 9999, ''),
    staleTime: 5 * 60_000,
  });

  const paymentMethodOptions = useEnumValues('PaymentMethod');
  const paymentStatusOptions = useEnumValues('PaymentStatus');
  const deliveryMethodOptions = useEnumValues('DeliveryMethod');
  const deliveryDayOptions = useEnumValues('DeliveryDay');
  const orderStatusOptions = useEnumValues('OrderStatus');

  // Next order number for a NEW order
  const { data: nextNumberData } = useQuery({
    queryKey: ['orders', 'nextNumber'],
    queryFn: OrderCompositeApi.getNextOrderNumber,
    enabled: !isEditMode,
    staleTime: 0,
    gcTime: 0,
  });

  // ── Form ──
  const { control, handleSubmit, reset, watch, setValue, formState: { errors, isDirty } } = useForm<OrderFormData>({
    resolver: zodResolver(orderSchema),
    defaultValues: {
      orderNumber: 0,
      fkCustomerId: 0,
      orderDate: nowLocalDateTime(),
      // Resolved by name below once the enum list arrives. Do NOT put a literal
      // here: TargCC numbers enum members alphabetically, and 1 is Cancelled.
      enmOrderStatus: undefined,
    },
  });

  const selectedCustomerId = watch('fkCustomerId');

  // The customer's class decides which price every line gets, so it must be
  // visible once a customer is chosen — otherwise the user has no way to tell
  // whether a farmer or a retail price is about to be applied.
  const selectedCustomer = useMemo(
    () => customerData?.items?.find((c: Customer) => c.id === selectedCustomerId),
    [customerData, selectedCustomerId]
  );
  const customerTypeOptions = useEnumValues('CustomerType');
  const selectedCustomerTypeLabel = useMemo(() => {
    if (!selectedCustomer) return '';
    const raw = (selectedCustomer as any).enmCustomerType;
    if (raw == null || raw === '') return '';
    // The API may hand back either the numeric enum value or its English name.
    const byValue = customerTypeOptions.find(o => o.value === Number(raw));
    if (byValue) return byValue.label;
    const byName = customerTypeOptions.find(
      o => o.name?.toLowerCase() === String(raw).toLowerCase()
    );
    return byName ? byName.label : String(raw);
  }, [selectedCustomer, customerTypeOptions]);

  // Enum defaults for a NEW order, resolved by name so they survive
  // regeneration. Until the enum list loads these are undefined and the effect
  // below simply doesn't fire — better a blank select for a moment than a
  // silently wrong status.
  const statusNew = useEnumValueByName('OrderStatus', 'New');
  const paymentPending = useEnumValueByName('PaymentStatus', 'Pending');
  const currentStatus = watch('enmOrderStatus');

  useEffect(() => {
    if (isEditMode) return;
    if (currentStatus === undefined && statusNew !== undefined) {
      setValue('enmOrderStatus', statusNew);
    }
  }, [isEditMode, currentStatus, statusNew, setValue]);

  useEffect(() => {
    if (isEditMode || paymentPending === undefined) return;
    setValue('enmPaymentStatus', paymentPending);
  }, [isEditMode, paymentPending, setValue]);

  // Prefill order number on create
  useEffect(() => {
    if (!isEditMode && nextNumberData?.nextOrderNumber) {
      setValue('orderNumber', nextNumberData.nextOrderNumber);
    }
  }, [isEditMode, nextNumberData, setValue]);

  useEffect(() => {
    if (isEditMode && orderData) {
      reset({
        orderNumber: orderData.orderNumber,
        fkCustomerId: orderData.fkCustomerId,
        orderDate: orderData.orderDate,
        enmPaymentMethod: orderData.enmPaymentMethod ?? 0,
        enmPaymentStatus: orderData.enmPaymentStatus ?? 0,
        paymentDate: orderData.paymentDate ?? '',
        invoiceNumber: orderData.invoiceNumber ?? '',
        enmDeliveryMethod: orderData.enmDeliveryMethod ?? 0,
        deliveryDate: orderData.deliveryDate ?? '',
        enmDeliveryDay: orderData.enmDeliveryDay ?? 0,
        enmOrderStatus: orderData.enmOrderStatus ?? 0,
        notes: orderData.notes ?? '',
        notes2: orderData.notes2 ?? '',
      });
    }
  }, [isEditMode, orderData, reset]);

  // ── Lines State ──
  const [lines, setLines] = useState<LineRow[]>([]);
  // Track lines the user changed (add/remove/update) for the unsaved-changes guard
  const [linesDirty, setLinesDirty] = useState(false);
  // IDs of existing (persisted) lines the user removed — sent to the server on save
  const [deletedLineIds, setDeletedLineIds] = useState<number[]>([]);
  // Monotonic negative counter for temp row ids — a ref so rapid double-clicks can't collide
  const tempIdCounter = useRef(0);
  // Latest lines snapshot for async price-resolution guards
  const linesRef = useRef<LineRow[]>(lines);
  linesRef.current = lines;

  useEffect(() => {
    if (orderLines?.items) {
      setLines(orderLines.items.map((l: OrderLine) => ({
        id: l.id,
        fkProductId: l.fkProductId,
        productName: productData?.items?.find((p: Product) => p.id === l.fkProductId)?.productName ?? '',
        quantity: l.quantity,
        unitPrice: l.unitPrice,
        discountPercent: l.discountPercent ?? 0,
        lineNumber: l.lineNumber,
        lineTotal: round2(l.lineTotal ?? (l.quantity * l.unitPrice * (1 - (l.discountPercent ?? 0) / 100))),
        isNew: false,
        isDirty: false,
      })));
    }
  }, [orderLines, productData]);

  // ── Add line ──
  const addLine = useCallback(() => {
    tempIdCounter.current -= 1;
    const newId = tempIdCounter.current;
    setLines(prev => [
      ...prev,
      {
        id: newId,
        fkProductId: 0,
        quantity: 1,
        unitPrice: 0,
        discountPercent: 0,
        lineNumber: prev.length + 1,
        lineTotal: 0,
        isNew: true,
        isDirty: true,
      },
    ]);
    setLinesDirty(true);
  }, []);

  const removeLine = useCallback((lineId: number) => {
    // Existing (persisted) lines must be reported to the server as deleted
    if (lineId > 0) {
      setDeletedLineIds(prev => (prev.includes(lineId) ? prev : [...prev, lineId]));
    }
    setLines(prev => prev.filter(l => l.id !== lineId));
    setLinesDirty(true);
  }, []);

  const updateLine = useCallback((lineId: number, field: keyof LineRow, value: unknown) => {
    setLines(prev => prev.map(l => {
      if (l.id !== lineId) return l;
      const updated = { ...l, [field]: value, isDirty: true };
      // Recalculate line total
      if (field === 'quantity' || field === 'unitPrice' || field === 'discountPercent') {
        const qty = field === 'quantity' ? Number(value) : updated.quantity;
        const price = field === 'unitPrice' ? Number(value) : updated.unitPrice;
        const disc = field === 'discountPercent' ? Number(value) : updated.discountPercent;
        updated.lineTotal = round2(qty * price * (1 - disc / 100));
      }
      return updated;
    }));
    setLinesDirty(true);
  }, []);

  // Auto-fill price when product selected (via /api/pricing/resolve)
  const handleProductChange = useCallback(async (lineId: number, productId: number) => {
    updateLine(lineId, 'fkProductId', productId);
    const product = productData?.items?.find((p: Product) => p.id === productId);
    if (product) {
      updateLine(lineId, 'productName', product.productName);
    }
    // Try to fetch customer-specific price
    if (selectedCustomerId && productId) {
      const quantity = linesRef.current.find(l => l.id === lineId)?.quantity ?? 1;
      try {
        const result = await PricingApi.resolve(productId, selectedCustomerId, quantity);
        // Stale-response guard: only apply if the row still exists and its product hasn't changed
        const row = linesRef.current.find(l => l.id === lineId);
        if (!row || row.fkProductId !== productId) return;
        if (result.found && result.unitPrice != null) {
          updateLine(lineId, 'unitPrice', result.unitPrice);
          if (result.discountPercent != null) {
            updateLine(lineId, 'discountPercent', result.discountPercent);
          }
        } else {
          // No price row for this (product, customer type). Silently leaving 0
          // looks like the auto-pricing is broken, when in fact the price list
          // simply has no entry — say so and let the user type one.
          showWarning(
            `למוצר "${product?.productName ?? ''}" אין מחיר מוגדר לסוג הלקוח `
            + `${selectedCustomerTypeLabel || ''} — יש להזין מחיר ידנית.`
          );
        }
      } catch { /* fallback: no auto-price */ }
    }
  }, [productData, selectedCustomerId, selectedCustomerTypeLabel, showWarning, updateLine]);

  // ── Totals ──
  const totals = useMemo(() => {
    const subtotal = round2(lines.reduce((sum, l) => sum + l.lineTotal, 0));
    const vat = round2(subtotal * (vatRate / 100));
    return { subtotal, vat, total: round2(subtotal + vat), lineCount: lines.length };
  }, [lines, vatRate]);

  // ── Save (atomic, via /api/orders/composite) ──
  const saveMutation = useMutation({
    mutationFn: async (formData: OrderFormData) => {
      const linePayloads: CompositeLinePayload[] = lines.map((line) => ({
        id: line.id > 0 ? line.id : 0, // temp/new lines are sent with id 0
        fkOrderHeaderId: isEditMode ? parsedId : 0,
        fkProductId: line.fkProductId,
        quantity: line.quantity,
        unitPrice: line.unitPrice,
        discountPercent: line.discountPercent,
        lineNumber: line.lineNumber,
      }));

      if (isEditMode) {
        const res = await OrderCompositeApi.updateComposite(parsedId, {
          header: formData as any,
          lines: linePayloads,
          deletedLineIds,
        });
        return { orderId: parsedId, errors: res.errors ?? [] };
      }
      const res = await OrderCompositeApi.createComposite({
        header: formData as any,
        lines: linePayloads,
        deletedLineIds: [],
      });
      return { orderId: res.header?.id ?? 0, errors: res.errors ?? [] };
    },
    onSuccess: ({ orderId, errors: lineErrors }) => {
      if (lineErrors.length > 0) {
        // Partial success (207) — surface the per-line errors
        showWarning(`${t.orderComposite.partialSaveErrors}: ${lineErrors.join(' | ')}`);
      } else {
        showSuccess(isEditMode ? t.messages.updated : t.messages.created);
      }
      queryClient.invalidateQueries({ queryKey: ['orderHeaders'] });
      queryClient.invalidateQueries({ queryKey: ['orderLines'] });
      queryClient.invalidateQueries({ queryKey: ['orders'] });
      if (orderId > 0) {
        navigate(`/orders/${orderId}`);
      }
    },
    onError: (error: unknown) => {
      // 422 — validation failed, stay on the form and show the server's per-line errors
      const axiosErr = error as { response?: { status?: number; data?: { message?: string; errors?: string[] } } };
      const data = axiosErr.response?.data;
      if (axiosErr.response?.status === 422 && data) {
        const parts = [data.message, ...(data.errors ?? [])].filter(Boolean);
        showError(parts.length > 0 ? parts.join(' | ') : t.messages.error);
        return;
      }
      const message = (error as Error)?.message;
      showError(message || t.messages.error);
    },
  });

  // Validate lines before save: block lines without a product or with quantity <= 0
  const submitForm = useCallback((formData: OrderFormData) => {
    const invalid = lines.some(l => l.fkProductId <= 0 || l.quantity <= 0);
    if (invalid) {
      showError(t.orderComposite.invalidLines);
      return;
    }
    saveMutation.mutate(formData);
  }, [lines, saveMutation, showError, t]);

  // Keep a stable reference for the keyboard shortcut
  const submitRef = useRef<() => void>(() => {});
  submitRef.current = () => handleSubmit(submitForm)();

  // Ctrl+S / Cmd+S saves the form
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === 's') {
        e.preventDefault();
        submitRef.current();
      }
    };
    window.addEventListener('keydown', handleKeyDown);
    return () => window.removeEventListener('keydown', handleKeyDown);
  }, []);

  // Unsaved-changes guard for Cancel / back navigation
  const hasUnsavedChanges = isDirty || linesDirty;
  const handleCancel = useCallback(() => {
    if (hasUnsavedChanges && !saveMutation.isPending) {
      if (!window.confirm(t.messages.unsavedChanges)) return;
    }
    navigate('/orders');
  }, [hasUnsavedChanges, saveMutation.isPending, navigate, t]);

  // ── Line columns ──
  const lineColumns: GridColDef[] = [
    { field: 'lineNumber', headerName: '#', width: 60 },
    {
      field: 'fkProductId',
      headerName: t.fields.productName,
      flex: 1,
      minWidth: 200,
      renderCell: (params) => {
        const products = productData?.items ?? [];
        return (
          <Autocomplete
            size="small"
            fullWidth
            options={products}
            getOptionLabel={(opt: any) => opt.productName || ''}
            value={products.find((p: any) => p.id === params.value) ?? undefined}
            onChange={(_, v) => v && handleProductChange(params.row.id, (v as any).id)}
            renderInput={(p) => <TextField {...p} variant="standard" placeholder={t.orderComposite.selectProduct} />}
            disableClearable
            sx={{ '& .MuiInput-root': { fontSize: '0.875rem' } }}
          />
        );
      },
    },
    {
      field: 'quantity',
      headerName: t.fields.quantity,
      width: 100,
      editable: false,
      renderCell: (params) => (
        <TextField
          type="number" variant="standard" size="small"
          value={params.value}
          onChange={(e) => updateLine(params.row.id, 'quantity', Number(e.target.value))}
          slotProps={{ input: { inputProps: { min: 0, step: 1, style: { textAlign: 'center' } } } }}
        />
      ),
    },
    {
      field: 'unitPrice',
      headerName: t.fields.unitPrice,
      width: 120,
      renderCell: (params) => (
        <TextField
          type="number" variant="standard" size="small"
          value={params.value}
          onChange={(e) => updateLine(params.row.id, 'unitPrice', Number(e.target.value))}
          slotProps={{ input: { inputProps: { min: 0, step: 0.01, style: { textAlign: 'center' } } } }}
        />
      ),
    },
    {
      field: 'discountPercent',
      headerName: t.fields.discountPercent,
      width: 100,
      renderCell: (params) => (
        <TextField
          type="number" variant="standard" size="small"
          value={params.value}
          onChange={(e) => updateLine(params.row.id, 'discountPercent', Number(e.target.value))}
          slotProps={{ input: { inputProps: { min: 0, max: 100, style: { textAlign: 'center' } } } }}
        />
      ),
    },
    {
      field: 'lineTotal',
      headerName: t.fields.lineTotal,
      width: 130,
      renderCell: (params) => (
        <Typography fontWeight={600} dir="ltr">
          ₪{Number(params.value).toLocaleString('he-IL', { minimumFractionDigits: 2 })}
        </Typography>
      ),
    },
    {
      field: 'actions',
      headerName: '',
      width: 60,
      sortable: false,
      renderCell: (params) => (
        <Tooltip title={t.actions.removeLine}>
          <IconButton size="small" color="error" onClick={() => removeLine(params.row.id)}>
            <DeleteIcon fontSize="small" />
          </IconButton>
        </Tooltip>
      ),
    },
  ];

  if (isEditMode && (orderLoading || linesLoading)) {
    return (
      <Box p={3}>
        <Skeleton variant="rectangular" height={60} sx={{ mb: 2, borderRadius: 2 }} />
        <Skeleton variant="rectangular" height={200} sx={{ mb: 2, borderRadius: 2 }} />
        <Skeleton variant="rectangular" height={300} sx={{ borderRadius: 2 }} />
      </Box>
    );
  }

  return (
    <Box>
      {/* Header Bar */}
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Box display="flex" alignItems="center" gap={2}>
          <IconButton onClick={handleCancel}>
            <ArrowForwardIcon />
          </IconButton>
          <Box>
            <Typography variant="h4">
              {isEditMode ? `${t.orderComposite.editOrder} #${orderData?.orderNumber}` : t.orderComposite.newOrder}
            </Typography>
            {isEditMode && orderData && (
              <Box display="flex" gap={1} mt={0.5}>
                <Chip
                  size="small" variant="outlined"
                  label={orderStatusOptions.find(o => o.value === orderData.enmOrderStatus)?.label ?? '—'}
                  color="primary"
                />
                <Chip
                  size="small"
                  label={paymentStatusOptions.find(o => o.value === orderData.enmPaymentStatus)?.label ?? '—'}
                  color={orderData.enmPaymentStatus === 2 ? 'success' : 'warning'}
                />
              </Box>
            )}
          </Box>
        </Box>
        <Box display="flex" gap={1}>
          <Button variant="outlined" onClick={handleCancel}>
            {t.actions.cancel}
          </Button>
          <Button
            variant="contained" startIcon={<SaveIcon />}
            onClick={handleSubmit(submitForm)}
            disabled={saveMutation.isPending}
          >
            {saveMutation.isPending ? t.actions.saving : t.actions.save}
          </Button>
        </Box>
      </Box>

      <form>
        {/* Order Header Details */}
        <Paper sx={{ p: 3, mb: 3 }}>
          <Typography variant="h6" mb={2} display="flex" alignItems="center" gap={1}>
            <ReceiptIcon color="primary" />
            {t.orderComposite.orderDetails}
          </Typography>

          <Grid container spacing={2}>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="orderNumber" control={control} render={({ field }) => (
                <TextField {...field} label={t.fields.orderNumber} type="number" fullWidth required
                  error={Boolean(errors.orderNumber)} helperText={errors.orderNumber?.message} />
              )} />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="fkCustomerId" control={control} render={({ field }) => (
                <Autocomplete
                  options={customerData?.items ?? []}
                  getOptionLabel={(opt: any) => `${opt.customerCode} - ${opt.customerName}`}
                  value={(customerData?.items ?? []).find((c: any) => c.id === field.value) || null}
                  onChange={(_, v) => field.onChange(v ? (v as any).id : 0)}
                  renderInput={(params) => (
                    <TextField {...params} label={t.orderComposite.selectCustomer} required
                      error={Boolean(errors.fkCustomerId)} helperText={errors.fkCustomerId?.message} />
                  )}
                />
              )} />
              {/* The class is what selects the price for every line, so it is
                  surfaced right under the picker rather than left implicit. */}
              {selectedCustomer && (
                <Stack direction="row" spacing={1} alignItems="center" sx={{ mt: 1 }}>
                  <Chip
                    size="small"
                    color={selectedCustomerTypeLabel ? 'primary' : 'warning'}
                    label={selectedCustomerTypeLabel
                      ? `סיווג: ${selectedCustomerTypeLabel}`
                      : 'סיווג לקוח לא מוגדר'}
                  />
                  {!selectedCustomerTypeLabel && (
                    <Typography variant="caption" color="text.secondary">
                      לא יימשך מחיר אוטומטי
                    </Typography>
                  )}
                </Stack>
              )}
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="orderDate" control={control} render={({ field }) => (
                <TextField {...field}
                  value={String(field.value ?? '').substring(0, 16)}
                  label={t.fields.orderDate} type="datetime-local" fullWidth required
                  slotProps={{ inputLabel: { shrink: true } }}
                  error={Boolean(errors.orderDate)} helperText={errors.orderDate?.message} />
              )} />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="enmOrderStatus" control={control} render={({ field }) => (
                <TextField {...field} label={t.enums.orderStatus} select fullWidth>
                  {orderStatusOptions.map(opt => <MenuItem key={opt.value} value={opt.value}>{opt.label}</MenuItem>)}
                </TextField>
              )} />
            </Grid>

            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="enmPaymentMethod" control={control} render={({ field }) => (
                <TextField {...field} label={t.enums.paymentMethod} select fullWidth>
                  <MenuItem value={0}>—</MenuItem>
                  {paymentMethodOptions.map(opt => <MenuItem key={opt.value} value={opt.value}>{opt.label}</MenuItem>)}
                </TextField>
              )} />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="enmPaymentStatus" control={control} render={({ field }) => (
                <TextField {...field} label={t.enums.paymentStatus} select fullWidth>
                  <MenuItem value={0}>—</MenuItem>
                  {paymentStatusOptions.map(opt => <MenuItem key={opt.value} value={opt.value}>{opt.label}</MenuItem>)}
                </TextField>
              )} />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="enmDeliveryMethod" control={control} render={({ field }) => (
                <TextField {...field} label={t.enums.deliveryMethod} select fullWidth>
                  <MenuItem value={0}>—</MenuItem>
                  {deliveryMethodOptions.map(opt => <MenuItem key={opt.value} value={opt.value}>{opt.label}</MenuItem>)}
                </TextField>
              )} />
            </Grid>
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <Controller name="deliveryDate" control={control} render={({ field }) => (
                <TextField {...field}
                  value={String(field.value ?? '').substring(0, 10)}
                  label={t.fields.deliveryDate} type="date" fullWidth
                  slotProps={{ inputLabel: { shrink: true } }} />
              )} />
            </Grid>

            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller name="notes" control={control} render={({ field }) => (
                <TextField {...field} label={t.fields.notes} multiline rows={2} fullWidth />
              )} />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller name="invoiceNumber" control={control} render={({ field }) => (
                <TextField {...field} label={t.fields.invoiceNumber} fullWidth />
              )} />
            </Grid>
          </Grid>
        </Paper>

        {/* Order Lines */}
        <Paper sx={{ p: 3, mb: 3 }}>
          <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
            <Typography variant="h6">
              {t.orderComposite.orderLines} ({lines.length})
            </Typography>
            <Button variant="outlined" startIcon={<AddIcon />} onClick={addLine} size="small">
              {t.orderComposite.addProduct}
            </Button>
          </Box>

          {lines.length === 0 ? (
            <Box textAlign="center" py={6}>
              <Typography color="text.secondary" mb={2}>{t.orderComposite.noLines}</Typography>
              <Button variant="contained" startIcon={<AddIcon />} onClick={addLine}>
                {t.orderComposite.addProduct}
              </Button>
            </Box>
          ) : (
            <DataGrid
              rows={lines}
              columns={lineColumns}
              autoHeight
              hideFooter
              disableRowSelectionOnClick
              getRowId={(row) => row.id}
              sx={{
                '& .MuiDataGrid-cell': { py: 1 },
                '& .MuiDataGrid-columnHeaders': { fontWeight: 700 },
              }}
            />
          )}
        </Paper>

        {/* Summary / Totals */}
        <Paper sx={{ p: 3, background: (theme) =>
          theme.palette.mode === 'dark'
            ? 'linear-gradient(135deg, #1E293B 0%, #0F172A 100%)'
            : 'linear-gradient(135deg, #EFF6FF 0%, #F8FAFC 100%)'
        }}>
          <Grid container spacing={2} justifyContent="flex-end">
            <Grid size={{ xs: 12, sm: 6, md: 4 }}>
              <Typography variant="h6" mb={2}>{t.orderComposite.summary}</Typography>
              <Box display="flex" justifyContent="space-between" mb={1}>
                <Typography>{t.orderComposite.subtotal}</Typography>
                <Typography fontWeight={500} dir="ltr">
                  ₪{totals.subtotal.toLocaleString('he-IL', { minimumFractionDigits: 2 })}
                </Typography>
              </Box>
              <Box display="flex" justifyContent="space-between" mb={1}>
                <Typography color="text.secondary">{`${t.orderComposite.vat} (${vatRate}%)`}</Typography>
                <Typography color="text.secondary" dir="ltr">
                  ₪{totals.vat.toLocaleString('he-IL', { minimumFractionDigits: 2 })}
                </Typography>
              </Box>
              <Divider sx={{ my: 1.5 }} />
              <Box display="flex" justifyContent="space-between">
                <Typography variant="h5" fontWeight={700}>{t.orderComposite.grandTotal}</Typography>
                <Typography variant="h5" fontWeight={700} color="primary" dir="ltr">
                  ₪{totals.total.toLocaleString('he-IL', { minimumFractionDigits: 2 })}
                </Typography>
              </Box>
            </Grid>
          </Grid>
        </Paper>
      </form>
    </Box>
  );
}
