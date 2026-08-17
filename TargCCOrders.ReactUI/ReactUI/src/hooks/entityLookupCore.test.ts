import { describe, it, expect } from 'vitest';
import { buildLookup, fallbackLabel } from './entityLookupCore';

describe('fallbackLabel', () => {
  it('names a customer', () => expect(fallbackLabel('customer', 3)).toBe('לקוח #3'));
  it('names a product', () => expect(fallbackLabel('product', 7)).toBe('מוצר #7'));
  it('names an order', () => expect(fallbackLabel('orderHeader', 12)).toBe('הזמנה #12'));
});

describe('buildLookup', () => {
  const customers = [
    { id: 1, customerName: 'משק כהן' },
    { id: 2, customerName: 'חוות רימון' },
  ];

  it('resolves a known id to its name', () => {
    expect(buildLookup('customer', customers)(1)).toBe('משק כהן');
  });

  it('resolves each of several ids', () => {
    const lookup = buildLookup('customer', customers);
    expect(lookup(2)).toBe('חוות רימון');
  });

  it('falls back rather than returning empty for an unknown id', () => {
    expect(buildLookup('customer', customers)(99)).toBe('לקוח #99');
  });

  it('falls back when the list has not loaded', () => {
    expect(buildLookup('customer', undefined)(5)).toBe('לקוח #5');
  });

  it('labels an order by its order number, not its row id', () => {
    const orders = [{ id: 12, orderNumber: 1042 }];
    expect(buildLookup('orderHeader', orders)(12)).toBe('הזמנה #1042');
  });

  it('falls back when the display field is null', () => {
    expect(buildLookup('customer', [{ id: 1, customerName: null }])(1)).toBe('לקוח #1');
  });

  it('falls back when the display field is an empty string', () => {
    expect(buildLookup('customer', [{ id: 1, customerName: '' }])(1)).toBe('לקוח #1');
  });
});
