// entityLookupCore.ts — pure half of the foreign-key-to-name resolution.
//
// Separated from the react-query hook so the fallback wording and the map
// construction can be tested without mounting anything.

export type LookupEntity = 'customer' | 'product' | 'orderHeader';

interface Spec {
  /** Field carrying the human-readable name. */
  display: string;
  /** Noun used to build a label out of a bare number. */
  noun: string;
  /**
   * Whether the display value is itself a number and needs the noun in front.
   * A customer name stands on its own; an order number does not.
   */
  prefixed: boolean;
}

const SPECS: Record<LookupEntity, Spec> = {
  customer: { display: 'customerName', noun: 'לקוח', prefixed: false },
  product: { display: 'productName', noun: 'מוצר', prefixed: false },
  orderHeader: { display: 'orderNumber', noun: 'הזמנה', prefixed: true },
};

/**
 * Label shown when the id cannot be resolved — the list is still loading, or
 * the referenced row was deleted.
 *
 * Deliberately not an empty string: a blank cell reads as a fault, whereas
 * "לקוח #3" reads as data and keeps the id in front of the user.
 */
export function fallbackLabel(entity: LookupEntity, id: number): string {
  return `${SPECS[entity].noun} #${id}`;
}

/**
 * Build an id → name resolver.
 *
 * A Map rather than Array.find per cell: with 1,312 customers and 25 rows the
 * linear scan would run on every render of every row.
 */
export function buildLookup(
  entity: LookupEntity,
  items: readonly Record<string, unknown>[] | undefined
): (id: number) => string {
  const spec = SPECS[entity];
  const map = new Map<number, string>();
  for (const item of items ?? []) {
    const value = item[spec.display];
    if (value === null || value === undefined || value === '') continue;
    map.set(Number(item.id), spec.prefixed ? `${spec.noun} #${value}` : String(value));
  }
  return (id: number) => map.get(id) ?? fallbackLabel(entity, id);
}
