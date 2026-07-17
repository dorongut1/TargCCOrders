// SettingsApi.ts
// Business settings endpoint (GET /api/settings/business)
import { api } from './client';

export interface BusinessSettings {
  vatRatePercent: number;
  debtAmountThreshold: number;
  debtOverdueDays: number;
  supplierEmailBiobee: string;
}

export const DEFAULT_BUSINESS_SETTINGS: BusinessSettings = {
  vatRatePercent: 18,
  debtAmountThreshold: 100,
  debtOverdueDays: 10,
  supplierEmailBiobee: '',
};

// The ASP.NET camelCase policy may serialize VATRatePercent inconsistently
// (e.g. "vatRatePercent" / "vATRatePercent"), so look keys up case-insensitively.
function getValue(data: Record<string, unknown>, key: string): string | undefined {
  const match = Object.keys(data).find((k) => k.toLowerCase() === key.toLowerCase());
  if (match === undefined) return undefined;
  const v = data[match];
  return v == null ? undefined : String(v);
}

function getNumber(data: Record<string, unknown>, key: string, fallback: number): number {
  const raw = getValue(data, key);
  const parsed = raw != null ? parseFloat(raw) : NaN;
  return Number.isFinite(parsed) ? parsed : fallback;
}

export const SettingsApi = {
  getBusiness: async (): Promise<BusinessSettings> => {
    const { data } = await api.get<Record<string, unknown>>('/settings/business');
    return {
      vatRatePercent: getNumber(data, 'vatRatePercent', DEFAULT_BUSINESS_SETTINGS.vatRatePercent),
      debtAmountThreshold: getNumber(data, 'debtAmountThreshold', DEFAULT_BUSINESS_SETTINGS.debtAmountThreshold),
      debtOverdueDays: getNumber(data, 'debtOverdueDays', DEFAULT_BUSINESS_SETTINGS.debtOverdueDays),
      supplierEmailBiobee: getValue(data, 'supplierEmailBiobee') ?? '',
    };
  },
};
