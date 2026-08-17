import { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  Box, Paper, Typography, Table, TableBody, TableCell, TableContainer, TableHead,
  TableRow, Button, IconButton, Chip, TextField, Dialog, DialogTitle, DialogContent,
  DialogActions, MenuItem, Alert, Tooltip, CircularProgress, Stack,
} from '@mui/material';
import LockOpenIcon from '@mui/icons-material/LockOpen';
import KeyIcon from '@mui/icons-material/Key';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import WarningAmberIcon from '@mui/icons-material/WarningAmber';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { UserAdminApi, type CreateUserPayload, type AdminUser } from '../api/UserAdminApi';
import { useNotification } from '../contexts/NotificationContext';

const emptyForm: CreateUserPayload = {
  userName: '', firstName: '', lastName: '', email: '', phoneNumber: '', roleId: 0,
};

export default function UserAdminList() {
  const qc = useQueryClient();
  const { showSuccess, showError, showWarning } = useNotification();
  const [search, setSearch] = useState('');
  const [createOpen, setCreateOpen] = useState(false);
  const [form, setForm] = useState<CreateUserPayload>(emptyForm);
  const [banner, setBanner] = useState<string | null>(null);
  // The edit dialog reuses the create dialog's fields; holding the id being
  // edited is the only extra state it needs.
  const [editId, setEditId] = useState<number | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<AdminUser | null>(null);

  const { data: me } = useQuery({ queryKey: ['userAdmin', 'me'], queryFn: UserAdminApi.me });
  const { data: users = [], isLoading } = useQuery({
    queryKey: ['userAdmin', 'users', search],
    queryFn: () => UserAdminApi.list(search),
    enabled: me?.canManageUsers === true,
  });
  const { data: roles = [] } = useQuery({
    queryKey: ['userAdmin', 'roles'],
    queryFn: UserAdminApi.roles,
    enabled: me?.canManageUsers === true,
  });

  const invalidate = () => qc.invalidateQueries({ queryKey: ['userAdmin', 'users'] });
  const fail = (e: any, fallback: string) =>
    showError(e?.response?.data?.message || fallback);

  const createMut = useMutation({
    mutationFn: UserAdminApi.create,
    onSuccess: (res) => {
      setCreateOpen(false);
      setForm(emptyForm);
      invalidate();
      // Shown as a persistent banner, not a toast: this is the only time the
      // manager sees the initial password, and a toast that disappears after
      // three seconds is the wrong place for something they must write down.
      setBanner(res.message);
      res.warnings?.forEach((w) => showWarning(w));
    },
    onError: (e) => fail(e, 'יצירת המשתמש נכשלה'),
  });

  const updateMut = useMutation({
    mutationFn: (v: { id: number; payload: CreateUserPayload }) =>
      UserAdminApi.update(v.id, v.payload),
    onSuccess: (res) => {
      setEditId(null);
      setForm(emptyForm);
      invalidate();
      showSuccess(res.message);
    },
    onError: (e) => fail(e, 'עדכון המשתמש נכשל'),
  });

  const deleteMut = useMutation({
    mutationFn: UserAdminApi.remove,
    onSuccess: (res) => {
      setDeleteTarget(null);
      invalidate();
      showSuccess(res.message);
    },
    onError: (e) => { setDeleteTarget(null); fail(e, 'מחיקת המשתמש נכשלה'); },
  });

  const startEdit = (u: AdminUser) => {
    setForm({
      userName: u.userName,
      firstName: u.firstName ?? '',
      lastName: u.lastName ?? '',
      email: u.email ?? '',
      phoneNumber: u.phoneNumber ?? '',
      roleId: u.roleId,
    });
    setEditId(u.id);
  };

  const resetMut = useMutation({
    mutationFn: UserAdminApi.resetPassword,
    onSuccess: (res) => { setBanner(res.message); invalidate(); },
    onError: (e) => fail(e, 'איפוס הסיסמה נכשל'),
  });

  const unlockMut = useMutation({
    mutationFn: UserAdminApi.unlock,
    onSuccess: (res) => { showSuccess(res.message); invalidate(); },
    onError: (e) => fail(e, 'הסרת החסימה נכשלה'),
  });

  if (me && !me.canManageUsers) {
    return (
      <Box sx={{ p: 3 }}>
        <Alert severity="warning">אין לך הרשאה לנהל משתמשים.</Alert>
      </Box>
    );
  }

  return (
    <Box sx={{ p: 3 }}>
      <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ mb: 2 }}>
        <Typography variant="h5">ניהול משתמשים</Typography>
        <Button variant="contained" startIcon={<PersonAddIcon />} onClick={() => setCreateOpen(true)}>
          משתמש חדש
        </Button>
      </Stack>

      {banner && (
        <Alert severity="info" onClose={() => setBanner(null)} sx={{ mb: 2 }}>
          {banner}
        </Alert>
      )}

      <TextField
        size="small"
        label="חיפוש"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        sx={{ mb: 2, width: 300 }}
      />

      <TableContainer component={Paper}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>שם משתמש</TableCell>
              <TableCell>שם מלא</TableCell>
              <TableCell>דוא"ל</TableCell>
              <TableCell>תפקיד</TableCell>
              <TableCell>סטטוס</TableCell>
              <TableCell align="center">פעולות</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {isLoading && (
              <TableRow><TableCell colSpan={6} align="center"><CircularProgress size={24} /></TableCell></TableRow>
            )}
            {!isLoading && users.length === 0 && (
              <TableRow><TableCell colSpan={6} align="center">לא נמצאו משתמשים</TableCell></TableRow>
            )}
            {users.map((u) => (
              <TableRow key={u.id} hover>
                <TableCell>{u.userName}</TableCell>
                <TableCell>{u.fullName}</TableCell>
                <TableCell>{u.email}</TableCell>
                <TableCell>{u.roleName}</TableCell>
                <TableCell>
                  <Stack direction="row" spacing={0.5}>
                    {u.isLockedOut && <Chip size="small" color="error" label="חסום" />}
                    {u.isDisabled && <Chip size="small" color="default" label="מושבת" />}
                    {!u.isLockedOut && !u.isDisabled && <Chip size="small" color="success" label="פעיל" />}
                    {/* Without application access TargCC rejects the login no
                        matter what password is used — worth flagging loudly. */}
                    {!u.hasApplicationAccess && (
                      <Tooltip title="המשתמש אינו משויך לאפליקציה ולא יוכל להתחבר">
                        <Chip size="small" color="warning" icon={<WarningAmberIcon />} label="ללא גישה" />
                      </Tooltip>
                    )}
                  </Stack>
                </TableCell>
                <TableCell align="center">
                  <Tooltip title="עריכת פרטים">
                    <IconButton size="small" onClick={() => startEdit(u)}><EditIcon fontSize="small" /></IconButton>
                  </Tooltip>
                  {/* Deleting yourself would end the session that could undo it,
                      so the button is disabled rather than left to fail server-side. */}
                  <Tooltip title={u.id === me?.userId ? 'לא ניתן למחוק את המשתמש שלך' : 'מחיקה'}>
                    <span>
                      <IconButton size="small" color="error" disabled={u.id === me?.userId}
                                  onClick={() => setDeleteTarget(u)}>
                        <DeleteIcon fontSize="small" />
                      </IconButton>
                    </span>
                  </Tooltip>
                  <Tooltip title="איפוס סיסמה">
                    <IconButton size="small" onClick={() => resetMut.mutate(u.id)}><KeyIcon fontSize="small" /></IconButton>
                  </Tooltip>
                  <Tooltip title="הסרת חסימה">
                    <span>
                      <IconButton size="small" disabled={!u.isLockedOut && !u.isDisabled}
                                  onClick={() => unlockMut.mutate(u.id)}>
                        <LockOpenIcon fontSize="small" />
                      </IconButton>
                    </span>
                  </Tooltip>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      {/* One dialog for both create and edit: the fields are identical, and
          keeping two copies is how they drift apart. */}
      <Dialog open={createOpen || editId !== null}
              onClose={() => { setCreateOpen(false); setEditId(null); setForm(emptyForm); }}
              maxWidth="sm" fullWidth>
        <DialogTitle>{editId !== null ? 'עריכת משתמש' : 'משתמש חדש'}</DialogTitle>
        <DialogContent>
          <Stack spacing={2} sx={{ mt: 1 }}>
            <TextField label="שם משתמש" required value={form.userName}
              onChange={(e) => setForm({ ...form, userName: e.target.value })} fullWidth />
            <Stack direction="row" spacing={2}>
              <TextField label="שם פרטי" value={form.firstName}
                onChange={(e) => setForm({ ...form, firstName: e.target.value })} fullWidth />
              <TextField label="שם משפחה" value={form.lastName}
                onChange={(e) => setForm({ ...form, lastName: e.target.value })} fullWidth />
            </Stack>
            <TextField label='דוא"ל' value={form.email}
              onChange={(e) => setForm({ ...form, email: e.target.value })} fullWidth />
            <TextField label="טלפון" value={form.phoneNumber}
              onChange={(e) => setForm({ ...form, phoneNumber: e.target.value })} fullWidth />
            <TextField label="תפקיד" required select value={form.roleId || ''}
              onChange={(e) => setForm({ ...form, roleId: Number(e.target.value) })} fullWidth>
              {roles.map((r) => <MenuItem key={r.id} value={r.id}>{r.name}</MenuItem>)}
            </TextField>
            {editId === null ? (
              <Alert severity="info">
                הסיסמה ההתחלתית תהיה <strong>1234</strong>. המשתמש יוכל לשנות אותה בעצמו במסך "שינוי סיסמה".
              </Alert>
            ) : editId === me?.userId ? (
              <Alert severity="warning">
                לא ניתן לשנות את התפקיד של עצמך — כדי שלא תישאר בלי גישה לניהול המשתמשים.
              </Alert>
            ) : null}
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => { setCreateOpen(false); setEditId(null); setForm(emptyForm); }}>ביטול</Button>
          <Button variant="contained"
            disabled={!form.userName.trim() || !form.roleId || createMut.isPending || updateMut.isPending}
            onClick={() => editId !== null
              ? updateMut.mutate({ id: editId, payload: form })
              : createMut.mutate(form)}>
            {editId !== null ? 'שמור' : 'צור משתמש'}
          </Button>
        </DialogActions>
      </Dialog>

      <Dialog open={deleteTarget !== null} onClose={() => setDeleteTarget(null)} maxWidth="xs" fullWidth>
        <DialogTitle>מחיקת משתמש</DialogTitle>
        <DialogContent>
          <Typography>
            למחוק את <strong>{deleteTarget?.userName}</strong>
            {deleteTarget?.fullName ? ` (${deleteTarget.fullName})` : ''}? הפעולה אינה הפיכה.
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteTarget(null)}>ביטול</Button>
          <Button variant="contained" color="error" disabled={deleteMut.isPending}
            onClick={() => deleteTarget && deleteMut.mutate(deleteTarget.id)}>
            מחק
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
