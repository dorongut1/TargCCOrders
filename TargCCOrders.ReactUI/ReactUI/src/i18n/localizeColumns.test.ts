import { describe, it, expect } from 'vitest';
import { localizeColumns, hasHebrew } from './localizeColumns';

describe('hasHebrew', () => {
  it('detects Hebrew', () => expect(hasHebrew('שם לקוח')).toBe(true));
  it('rejects English', () => expect(hasHebrew('Customer Name')).toBe(false));
  it('treats undefined as not Hebrew', () => expect(hasHebrew(undefined)).toBe(false));
});

describe('localizeColumns', () => {
  it('translates a known English header', () => {
    const out = localizeColumns([{ field: 'customerName', headerName: 'Customer Name' }]);
    expect(out[0].headerName).toBe('שם לקוח');
  });

  it('leaves a caller-supplied Hebrew header alone', () => {
    const out = localizeColumns([{ field: 'customerName', headerName: 'לקוח משלם' }]);
    expect(out[0].headerName).toBe('לקוח משלם');
  });

  it('leaves an unknown field alone', () => {
    const out = localizeColumns([{ field: 'mysteryField', headerName: 'Mystery' }]);
    expect(out[0].headerName).toBe('Mystery');
  });

  it('supplies a header when the column has none', () => {
    const out = localizeColumns([{ field: 'fkCustomerId' }]);
    expect(out[0].headerName).toBe('שם לקוח');
  });

  it('translates a foreign key to the thing it points at', () => {
    const out = localizeColumns([{ field: 'fkCustomerId', headerName: 'Customer ID' }]);
    expect(out[0].headerName).toBe('שם לקוח');
  });

  it('does not mutate the input', () => {
    const input = [{ field: 'customerName', headerName: 'Customer Name' }];
    localizeColumns(input);
    expect(input[0].headerName).toBe('Customer Name');
  });

  it('returns a new array', () => {
    const input = [{ field: 'customerName', headerName: 'Customer Name' }];
    expect(localizeColumns(input)).not.toBe(input);
  });
});
