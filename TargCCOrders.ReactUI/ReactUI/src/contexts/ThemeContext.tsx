// ThemeContext.tsx
// TargCCOrders — Modern MUI Theme with RTL Hebrew Support
import { createContext, useContext, useState, useMemo, useEffect, type ReactNode } from 'react';
import { ThemeProvider as MuiThemeProvider, createTheme, alpha } from '@mui/material/styles';
import { heIL } from '@mui/material/locale';
import { heIL as dataGridHeIL } from '@mui/x-data-grid/locales';
import CssBaseline from '@mui/material/CssBaseline';
import rtlPlugin from 'stylis-plugin-rtl';
import { CacheProvider } from '@emotion/react';
import createCache from '@emotion/cache';

const cacheRtl = createCache({ key: 'muirtl', stylisPlugins: [rtlPlugin], prepend: true });

interface ThemeContextType {
  isDarkMode: boolean;
  toggleTheme: () => void;
}

const ThemeContext = createContext<ThemeContextType | null>(null);

export function useThemeMode() {
  const ctx = useContext(ThemeContext);
  if (!ctx) throw new Error('useThemeMode must be used within AppThemeProvider');
  return ctx;
}

// ── Modern color palette ──
const brandColors = {
  primary: '#2563EB',      // Vibrant blue
  secondary: '#7C3AED',    // Purple accent
  success: '#059669',      // Emerald
  warning: '#D97706',      // Amber
  error: '#DC2626',        // Red
  info: '#0891B2',         // Cyan
};

export function AppThemeProvider({ children }: { children: ReactNode }) {
  const [isDarkMode, setIsDarkMode] = useState(() => {
    const saved = localStorage.getItem('theme_mode');
    if (saved) return saved === 'dark';
    return window.matchMedia?.('(prefers-color-scheme: dark)').matches ?? false;
  });

  const toggleTheme = () => {
    setIsDarkMode((prev) => {
      const next = !prev;
      localStorage.setItem('theme_mode', next ? 'dark' : 'light');
      return next;
    });
  };

  const theme = useMemo(
    () =>
      createTheme({
        direction: 'rtl',
        palette: {
          mode: isDarkMode ? 'dark' : 'light',
          primary: { main: brandColors.primary },
          secondary: { main: brandColors.secondary },
          success: { main: brandColors.success },
          warning: { main: brandColors.warning },
          error: { main: brandColors.error },
          info: { main: brandColors.info },
          ...(isDarkMode
            ? {
                background: {
                  default: '#0F172A',   // Slate-900
                  paper: '#1E293B',     // Slate-800
                },
              }
            : {
                background: {
                  default: '#F8FAFC',   // Slate-50
                  paper: '#FFFFFF',
                },
              }),
        },
        typography: {
          fontFamily: '"Rubik", "Heebo", "Assistant", "Segoe UI", Arial, sans-serif',
          h4: { fontWeight: 700, letterSpacing: '-0.02em' },
          h5: { fontWeight: 600, letterSpacing: '-0.01em' },
          h6: { fontWeight: 600 },
          subtitle1: { fontWeight: 500 },
          body2: { fontSize: '0.875rem' },
          button: { fontWeight: 600 },
        },
        shape: {
          borderRadius: 12,
        },
        components: {
          MuiCssBaseline: {
            styleOverrides: {
              '@import': "url('https://fonts.googleapis.com/css2?family=Rubik:wght@300;400;500;600;700&display=swap')",
              body: {
                scrollbarWidth: 'thin',
                scrollbarColor: isDarkMode ? '#475569 transparent' : '#CBD5E1 transparent',
              },
            },
          },
          MuiAppBar: {
            defaultProps: { elevation: 0 },
            styleOverrides: {
              root: {
                backdropFilter: 'blur(12px)',
                backgroundColor: isDarkMode
                  ? alpha('#0F172A', 0.85)
                  : alpha('#FFFFFF', 0.85),
                borderBottom: `1px solid ${isDarkMode ? '#334155' : '#E2E8F0'}`,
                color: isDarkMode ? '#F1F5F9' : '#1E293B',
              },
            },
          },
          MuiDrawer: {
            styleOverrides: {
              paper: {
                borderRight: 'none',
                borderLeft: `1px solid ${isDarkMode ? '#334155' : '#E2E8F0'}`,
                backgroundColor: isDarkMode ? '#1E293B' : '#FFFFFF',
              },
            },
          },
          MuiPaper: {
            defaultProps: { elevation: 0 },
            styleOverrides: {
              root: {
                border: `1px solid ${isDarkMode ? '#334155' : '#E2E8F0'}`,
                backgroundImage: 'none',
              },
            },
          },
          MuiCard: {
            defaultProps: { elevation: 0 },
            styleOverrides: {
              root: {
                border: `1px solid ${isDarkMode ? '#334155' : '#E2E8F0'}`,
                transition: 'transform 0.2s ease, box-shadow 0.2s ease',
                '&:hover': {
                  transform: 'translateY(-2px)',
                  boxShadow: isDarkMode
                    ? '0 8px 25px rgba(0,0,0,0.3)'
                    : '0 8px 25px rgba(0,0,0,0.08)',
                },
              },
            },
          },
          MuiButton: {
            defaultProps: { disableElevation: true },
            styleOverrides: {
              root: {
                borderRadius: 8,
                textTransform: 'none',
                fontWeight: 600,
                padding: '8px 20px',
              },
              containedPrimary: {
                background: `linear-gradient(135deg, ${brandColors.primary} 0%, ${brandColors.secondary} 100%)`,
                '&:hover': {
                  background: `linear-gradient(135deg, ${alpha(brandColors.primary, 0.9)} 0%, ${alpha(brandColors.secondary, 0.9)} 100%)`,
                },
              },
            },
          },
          MuiTextField: {
            defaultProps: { variant: 'outlined', size: 'small' },
            styleOverrides: {
              root: {
                '& .MuiOutlinedInput-root': {
                  borderRadius: 8,
                },
              },
            },
          },
          MuiChip: {
            styleOverrides: {
              root: { fontWeight: 500, borderRadius: 8 },
            },
          },
          MuiTooltip: {
            defaultProps: { arrow: true },
            styleOverrides: {
              tooltip: { borderRadius: 6, fontSize: '0.8rem' },
            },
          },
          MuiDialog: {
            styleOverrides: {
              paper: { borderRadius: 16 },
            },
          },
          // DataGrid styling is applied via sx props in AppDataGrid component
        },
      },
      // Hebrew component texts (MUI core + DataGrid)
      heIL,
      dataGridHeIL),
    [isDarkMode]
  );

  useEffect(() => {
    document.dir = 'rtl';
    document.documentElement.lang = 'he';
  }, []);

  return (
    <CacheProvider value={cacheRtl}>
    <ThemeContext.Provider value={{ isDarkMode, toggleTheme }}>
      <MuiThemeProvider theme={theme}>
        <CssBaseline />
        {children}
      </MuiThemeProvider>
    </ThemeContext.Provider>
    </CacheProvider>
  );
}

