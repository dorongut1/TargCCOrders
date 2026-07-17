// useTranslation.ts — Simple translation hook
// Usage: const { t } = useTranslation();
//        t.fields.orderNumber → 'מספר הזמנה'
//        t.actions.save → 'שמור'

import he from './he';

export function useTranslation() {
  return { t: he, lang: 'he' as const };
}

// Shorthand helper for field labels
export function fieldLabel(key: string): string {
  const labels = he.fields as Record<string, string>;
  return labels[key] ?? key;
}

// Shorthand helper for entity labels
export function entityLabel(entity: string, plural = false): string {
  const entities = he.entities as Record<string, { s: string; p: string }>;
  const entry = entities[entity];
  if (!entry) return entity;
  return plural ? entry.p : entry.s;
}

export default useTranslation;
