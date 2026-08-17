// useEntityLookup.ts — id → display name for the three foreign keys in the
// schema.
//
// Reuses the query keys the composite order screen already uses, so
// react-query serves this from the shared cache and no extra request is made
// on screens that have already loaded the list.
//
// This is done on the client on purpose. Adding name columns to the stored
// procedures would be the ordinal-drift pattern that has broken this project
// three times: the VB layer reads columns by position and can no longer be
// regenerated. See HEBREW_SCREENS_DESIGN_2026-08-17.md.
import { useMemo } from 'react';
import { useQuery } from '@tanstack/react-query';
import { CustomerApi } from '../api/CustomerApi';
import { ProductApi } from '../api/ProductApi';
import { OrderHeaderApi } from '../api/OrderHeaderApi';
import { buildLookup, type LookupEntity } from './entityLookupCore';

/**
 * The three list endpoints share a shape but not a row type, so the rows are
 * widened to `unknown` here. buildLookup reads them by field name anyway, and
 * without the widening the union of the three query functions has no common
 * type react-query will accept.
 */
interface Source {
  key: readonly string[];
  fn: () => Promise<{ items: unknown[]; total: number }>;
}

// First argument is a page number, not an offset, so page 0 with a large page
// size returns everything.
const SOURCES: Record<LookupEntity, Source> = {
  customer: { key: ['customers', 'all'], fn: () => CustomerApi.getAll(0, 9999, '') },
  product: { key: ['products', 'all'], fn: () => ProductApi.getAll(0, 9999, '') },
  orderHeader: { key: ['orderHeaders', 'all'], fn: () => OrderHeaderApi.getAll(0, 9999, '') },
};

export function useEntityLookup(entity: LookupEntity): (id: number) => string {
  const source = SOURCES[entity];
  const { data } = useQuery({
    queryKey: source.key,
    queryFn: source.fn,
    staleTime: 5 * 60_000,
  });
  return useMemo(
    () => buildLookup(entity, data?.items as Record<string, unknown>[] | undefined),
    [entity, data]
  );
}
