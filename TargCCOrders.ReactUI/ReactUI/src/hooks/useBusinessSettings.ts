// useBusinessSettings.ts
// Cached business settings (VAT rate etc.) with safe fallbacks.
import { useQuery } from '@tanstack/react-query';
import { SettingsApi, DEFAULT_BUSINESS_SETTINGS, type BusinessSettings } from '../api/SettingsApi';

export function useBusinessSettings() {
  const query = useQuery({
    queryKey: ['settings', 'business'],
    queryFn: SettingsApi.getBusiness,
    staleTime: Infinity,
    retry: 1,
  });
  // Fallback to defaults (VAT 18%) when the request fails or hasn't resolved yet.
  const settings: BusinessSettings = query.data ?? DEFAULT_BUSINESS_SETTINGS;
  return { ...query, settings };
}
