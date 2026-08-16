import { useState } from 'react';
import { useMutation } from '@tanstack/react-query';
import { Box, Paper, Typography, TextField, Button, Stack, Alert } from '@mui/material';
import { UserAdminApi } from '../api/UserAdminApi';
import { useNotification } from '../contexts/NotificationContext';

/**
 * Self-service password change. The server takes the target user from the JWT,
 * so this screen can only ever change the caller's own password — there is no
 * user id to tamper with.
 */
export default function ChangeMyPassword() {
  const { showSuccess, showError } = useNotification();
  const [current, setCurrent] = useState('');
  const [next, setNext] = useState('');
  const [confirm, setConfirm] = useState('');

  const mismatch = confirm.length > 0 && next !== confirm;
  const tooShort = next.length > 0 && next.length < 4;

  const mut = useMutation({
    mutationFn: () => UserAdminApi.changeMyPassword(current, next),
    onSuccess: (res) => {
      showSuccess(res.message);
      setCurrent(''); setNext(''); setConfirm('');
    },
    onError: (e: any) =>
      showError(e?.response?.data?.message || 'שינוי הסיסמה נכשל'),
  });

  return (
    <Box sx={{ p: 3, maxWidth: 480 }}>
      <Typography variant="h5" sx={{ mb: 2 }}>שינוי סיסמה</Typography>
      <Paper sx={{ p: 3 }}>
        <Stack spacing={2}>
          <TextField label="סיסמה נוכחית" type="password" value={current}
            onChange={(e) => setCurrent(e.target.value)} fullWidth autoComplete="current-password" />
          <TextField label="סיסמה חדשה" type="password" value={next}
            onChange={(e) => setNext(e.target.value)} fullWidth autoComplete="new-password"
            error={tooShort} helperText={tooShort ? 'לפחות 4 תווים' : ' '} />
          <TextField label="אימות סיסמה חדשה" type="password" value={confirm}
            onChange={(e) => setConfirm(e.target.value)} fullWidth autoComplete="new-password"
            error={mismatch} helperText={mismatch ? 'הסיסמאות אינן זהות' : ' '} />
          <Alert severity="info">
            לא ניתן להשתמש שוב באחת מארבע הסיסמאות האחרונות.
          </Alert>
          <Button variant="contained"
            disabled={!current || !next || mismatch || tooShort || mut.isPending}
            onClick={() => mut.mutate()}>
            שנה סיסמה
          </Button>
        </Stack>
      </Paper>
    </Box>
  );
}
