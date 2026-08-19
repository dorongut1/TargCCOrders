// ParametersList.tsx — manage how the system's lists are presented.
//
// One tab per managed list. Each row can be relabelled, reordered, hidden from
// new records, and — for delivery methods — marked as handled by the Deliver
// application.
//
// Adding a value is deliberately absent. The set of values comes from the
// compiled enum, so a new one is a code change; offering a button that could
// not work would be worse than not offering it. The note at the top of the
// screen says so rather than leaving the user to discover it.
import { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  Box, Paper, Typography, Table, TableBody, TableCell, TableContainer, TableHead,
  TableRow, IconButton, TextField, Dialog, DialogTitle, DialogContent, DialogActions,
  Button, Tabs, Tab, Alert, Chip, Switch, Tooltip, Stack, CircularProgress,
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import { ParametersApi, type ParameterValue } from '../api/ParametersApi';
import { useNotification } from '../contexts/NotificationContext';

export default function ParametersList() {
  const qc = useQueryClient();
  const { showSuccess, showError } = useNotification();
  const [activeTab, setActiveTab] = useState(0);
  const [editing, setEditing] = useState<ParameterValue | null>(null);
  const [form, setForm] = useState({ label: '', isActive: true, isDelivery: false, sortOrder: 0 });

  const { data: types = [], isLoading } = useQuery({
    queryKey: ['parameters'],
    queryFn: ParametersApi.list,
  });

  const updateMut = useMutation({
    mutationFn: (v: { type: string; value: string; payload: typeof form }) =>
      ParametersApi.update(v.type, v.value, v.payload),
    onSuccess: (res) => {
      setEditing(null);
      qc.invalidateQueries({ queryKey: ['parameters'] });
      // The dropdowns everywhere else read from this list, so they have to be
      // refetched too or the screen and the rest of the app disagree.
      qc.invalidateQueries({ queryKey: ['enums'] });
      showSuccess(res.message);
    },
    onError: (e: any) => showError(e?.response?.data?.message || 'העדכון נכשל'),
  });

  const startEdit = (value: ParameterValue) => {
    setForm({
      label: value.label,
      isActive: value.isActive,
      isDelivery: value.isDelivery,
      sortOrder: value.sortOrder,
    });
    setEditing(value);
  };

  if (isLoading) {
    return (
      <Box display="flex" justifyContent="center" p={4}>
        <CircularProgress />
      </Box>
    );
  }

  const current = types[activeTab];

  return (
    <Box sx={{ p: 3 }}>
      <Typography variant="h4" gutterBottom>ניהול פרמטרים</Typography>

      <Alert severity="info" sx={{ mb: 2 }}>
        כאן נקבע <strong>איך</strong> הרשימות מוצגות — שם, סדר, והאם ערך עדיין
        מוצע ברשומות חדשות. <strong>הוספת ערך חדש דורשת פיתוח</strong>, כי רשימת
        הערכים מוגדרת בקוד המערכת.
      </Alert>

      <Paper>
        <Tabs
          value={activeTab}
          onChange={(_, v) => setActiveTab(v)}
          variant="scrollable"
          scrollButtons="auto"
        >
          {types.map((t) => <Tab key={t.enumType} label={t.label} />)}
        </Tabs>

        {current && (
          <TableContainer>
            <Table size="small">
              <TableHead>
                <TableRow>
                  <TableCell>סדר</TableCell>
                  <TableCell>תווית</TableCell>
                  <TableCell>ערך במערכת</TableCell>
                  <TableCell align="center">בשימוש</TableCell>
                  <TableCell align="center">מוצע</TableCell>
                  {current.showDeliveryFlag && <TableCell align="center">דליבר</TableCell>}
                  <TableCell align="center">עריכה</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {current.values.map((v) => (
                  <TableRow key={v.enumValue} hover sx={{ opacity: v.isActive ? 1 : 0.5 }}>
                    <TableCell>{v.sortOrder}</TableCell>
                    <TableCell>
                      <Typography variant="body2" fontWeight={500}>
                        {v.label || <em>(ללא תווית)</em>}
                      </Typography>
                    </TableCell>
                    <TableCell>
                      <Typography variant="caption" color="text.secondary" dir="ltr">
                        {v.enumValue}
                      </Typography>
                    </TableCell>
                    <TableCell align="center">
                      {v.usageCount > 0
                        ? <Chip size="small" label={v.usageCount} variant="outlined" />
                        : <Typography variant="caption" color="text.disabled">—</Typography>}
                    </TableCell>
                    <TableCell align="center">
                      {v.isActive
                        ? <Chip size="small" color="success" label="כן" />
                        : <Chip size="small" label="מוסתר" />}
                    </TableCell>
                    {current.showDeliveryFlag && (
                      <TableCell align="center">
                        {v.isDelivery && (
                          <Tooltip title="נשלח לאפליקציית דליבר">
                            <LocalShippingIcon fontSize="small" color="primary" />
                          </Tooltip>
                        )}
                      </TableCell>
                    )}
                    <TableCell align="center">
                      <IconButton size="small" onClick={() => startEdit(v)}>
                        <EditIcon fontSize="small" />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        )}
      </Paper>

      <Dialog open={editing !== null} onClose={() => setEditing(null)} maxWidth="sm" fullWidth>
        <DialogTitle>עריכת ערך</DialogTitle>
        <DialogContent>
          <Stack spacing={2} sx={{ mt: 1 }}>
            <TextField
              label="תווית" required fullWidth value={form.label}
              onChange={(e) => setForm({ ...form, label: e.target.value })}
            />
            <TextField
              label="סדר תצוגה" type="number" fullWidth value={form.sortOrder}
              onChange={(e) => setForm({ ...form, sortOrder: Number(e.target.value) })}
              helperText="מספר נמוך מופיע קודם"
            />
            <Stack direction="row" alignItems="center" spacing={1}>
              <Switch
                checked={form.isActive}
                onChange={(e) => setForm({ ...form, isActive: e.target.checked })}
              />
              <Box>
                <Typography variant="body2">מוצע ברשומות חדשות</Typography>
                <Typography variant="caption" color="text.secondary">
                  ערך מוסתר נשאר תקין ברשומות קיימות — הוא רק לא יוצע מכאן והלאה
                </Typography>
              </Box>
            </Stack>
            {editing && types[activeTab]?.showDeliveryFlag && (
              <Stack direction="row" alignItems="center" spacing={1}>
                <Switch
                  checked={form.isDelivery}
                  onChange={(e) => setForm({ ...form, isDelivery: e.target.checked })}
                />
                <Box>
                  <Typography variant="body2">מנוהל באפליקציית דליבר</Typography>
                  <Typography variant="caption" color="text.secondary">
                    רק צורות משלוח מסומנות יישלחו לדליבר; השאר יוצגו בלוח בלבד
                  </Typography>
                </Box>
              </Stack>
            )}
            {editing && editing.usageCount > 0 && !form.isActive && (
              <Alert severity="warning">
                לערך הזה יש <strong>{editing.usageCount}</strong> רשומות קיימות.
                הן לא ייפגעו — הוא פשוט לא יוצע יותר ברשומות חדשות.
              </Alert>
            )}
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setEditing(null)}>ביטול</Button>
          <Button
            variant="contained"
            disabled={!form.label.trim() || updateMut.isPending}
            onClick={() => editing && updateMut.mutate({
              type: editing.enumType, value: editing.enumValue, payload: form,
            })}
          >
            שמור
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
