# תוכנית מימוש — עברות המסכים המיוצרים

> **לעובדים אוטונומיים:** יש להשתמש ב-superpowers:subagent-driven-development או
> superpowers:executing-plans ולבצע משימה-משימה. השלבים מסומנים ב-`- [ ]`.

**מטרה:** כל מסך שנגיש מהתפריט מוצג בעברית ומראה שמות במקום מזהי FK.

**ארכיטקטורה:** מילון מרכזי אחד (`he.ts`) מזין מודול תרגום טהור, ש-`AppDataGrid`
מפעיל על העמודות והכותרת של כל מסכי הרשימה בבת אחת. מזהי FK נפתרים בצד הלקוח
דרך הוק שמשתמש ב-cache הקיים של react-query — אפס נגיעה ב-DB ובשכבת ה-VB.

**סטאק:** React 18 · TypeScript · MUI X DataGrid · react-query · vitest (נוסף כאן)

**מסמך העיצוב:** `HEBREW_SCREENS_DESIGN_2026-08-17.md`

**מיקום עבודה:** `C:\Dev\NonTFS\TargCCOrders` על `main`.

---

## מפת קבצים

| קובץ | אחריות | פעולה |
|---|---|---|
| `src/i18n/he.ts` | מילון — מקור אמת יחיד לאוצר המילים | הרחבה |
| `src/i18n/fieldLabels.ts` | מיפוי שם-שדה ← תווית עברית | **חדש** |
| `src/i18n/localizeColumns.ts` | כלל היירוט, פונקציה טהורה | **חדש** |
| `src/i18n/localizeColumns.test.ts` | בדיקות לכלל היירוט | **חדש** |
| `src/hooks/useEntityLookup.ts` | פתרון FK ← שם | **חדש** |
| `src/hooks/entityLookupCore.ts` | בניית ה-Map והנפילות, טהור | **חדש** |
| `src/hooks/entityLookupCore.test.ts` | בדיקות ל-Map ולנפילות | **חדש** |
| `src/components/shared/AppDataGrid.tsx` | חיווט היירוט | שינוי |
| `src/routes.ts` | הסתרת `customerDebts` | שינוי |
| `src/pages/*List.tsx` (10) | מחרוזות מחוץ ל-Grid + עמודות FK | שינוי |
| `src/pages/*View.tsx` (10) | תוויות שדות | שינוי |
| `src/pages/*Form.tsx` (10) | תוויות שדות | שינוי |

**עשרת המסכים המיוצרים** (אומת בספירה): `BeehiveBuyerTracking` · `CustomerDebt` ·
`Customer` · `Delivery` · `OrderHeader` · `OrderLine` · `Product` ·
`ProductPriceHist` · `ProductPrice` · `SupplierOrder`.

`DebtManagement`, `DeliveryWorkflow`, `OrderComposite` ו-`UserAdmin` כתובים ביד,
כבר בעברית, ו**אין לגעת בהם** — הם הבקרה שלנו לרגרסיה.
| `src/types/ProductPriceHist.ts` | הסרת שדה רפאים | שינוי |

**למה `localizeColumns` ו-`entityLookupCore` נפרדים מהצרכנים שלהם:** הם הלוגיקה
היחידה בשינוי הזה ש-`tsc` לא יתפוס, והם היחידים שבאג בהם שובר מסכים שעובדים
היום. הפרדה לפונקציות טהורות היא מה שמאפשר לבדוק אותם בלי להרים דפדפן.

---

## Task 1: תשתית בדיקות

**קבצים:** שינוי `package.json`

- [ ] **שלב 1: התקנת vitest**

```bash
cd C:/Dev/NonTFS/TargCCOrders/TargCCOrders.ReactUI/ReactUI
npm install -D vitest@^2
```

- [ ] **שלב 2: הוספת הסקריפט**

ב-`package.json`, בתוך `"scripts"`, אחרי `"lint"`:

```json
    "test": "vitest run"
```

- [ ] **שלב 3: אימות שהרץ עובד**

```bash
npx vitest run --passWithNoTests
```

צפוי: `No test files found` ויציאה 0.

- [ ] **שלב 4: commit**

```bash
git add package.json package-lock.json
git commit -m "Add vitest for the two pure helpers the compiler cannot check"
```

---

## Task 2: מילון השדות

**קבצים:** שינוי `src/i18n/he.ts` · יצירה `src/i18n/fieldLabels.ts`

- [ ] **שלב 1: הרחבת `he.ts`**

ב-`fields`, לפני הסוגר המסיים, להוסיף:

```ts
    // Price list
    baseCost: 'עלות בסיס',
    sellingPrice: 'מחיר מכירה',
    minQuantity: 'כמות מינימלית',
    validFrom: 'בתוקף מ־',
    validTo: 'בתוקף עד',
    archivedDate: 'תאריך ארכוב',
    archivedReason: 'סיבת ארכוב',
    originalPriceId: 'מחיר מקורי',
    // Product
    currentStock: 'מלאי נוכחי',
    unitOfMeasure: 'יחידת מידה',
    // Customer
    customerIdentifier: 'מזהה לקוח',
    // Supplier order
    sentDate: 'תאריך שליחה',
    requestedDeliveryDate: 'תאריך אספקה מבוקש',
    requestedDeliveryDay: 'יום אספקה מבוקש',
    // Payment
    paymentDate: 'תאריך תשלום',
    // Beehive
    isRelevant: 'רלוונטי',
```

וב-`actions`, להוסיף את הפועל שחסר לכפתור הסינון:

```ts
    hideFilters: 'הסתר סינון',
```

- [ ] **שלב 2: יצירת `src/i18n/fieldLabels.ts`**

```ts
// fieldLabels.ts — API field name → Hebrew label.
// Single source: everything here resolves out of he.ts, so the dictionary
// stays the one place vocabulary is decided.
import he from './he';

/**
 * Foreign keys and enum columns do not map onto `fields` one-for-one: an
 * enum column is labelled by what it means (`enmPaymentMethod` → אמצעי תשלום)
 * and a foreign key by the thing it points at (`fkCustomerId` → שם לקוח).
 */
const extra: Record<string, string> = {
  fkCustomerId: he.fields.customerName,
  fkProductId: he.fields.productName,
  fkOrderHeaderId: he.fields.orderNumber,
  productId: he.fields.productName,
  enmPaymentMethod: he.enums.paymentMethod,
  enmPaymentStatus: he.enums.paymentStatus,
  enmDeliveryMethod: he.enums.deliveryMethod,
  enmDeliveryDay: he.enums.deliveryDay,
  enmOrderStatus: he.enums.orderStatus,
  enmDeliveryStatus: he.enums.deliveryStatus,
  enmDebtStatus: he.enums.debtStatus,
  enmCustomerType: he.enums.customerType,
  enmCategory: he.enums.category,
  enmAccountantMethod: he.enums.accountantMethod,
  enmEmailStatus: he.enums.emailStatus,
};

const labels: Record<string, string> = {
  ...(he.fields as Record<string, string>),
  ...extra,
};

/** Hebrew label for an API field name, or undefined if we have none. */
export function fieldLabel(field: string): string | undefined {
  return labels[field];
}
```

- [ ] **שלב 3: אימות הידור**

```bash
npx tsc --noEmit
```

צפוי: ללא פלט.

- [ ] **שלב 4: commit**

```bash
git add src/i18n/he.ts src/i18n/fieldLabels.ts
git commit -m "Add the field-name to Hebrew-label map"
```

---

## Task 3: כלל היירוט (TDD)

**קבצים:** יצירה `src/i18n/localizeColumns.ts` + `localizeColumns.test.ts`

- [ ] **שלב 1: כתיבת הבדיקה הנכשלת**

`src/i18n/localizeColumns.test.ts`:

```ts
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
```

- [ ] **שלב 2: הרצה לאימות כישלון**

```bash
npx vitest run src/i18n/localizeColumns.test.ts
```

צפוי: כישלון — `Failed to resolve import "./localizeColumns"`.

- [ ] **שלב 3: מימוש מינימלי**

`src/i18n/localizeColumns.ts`:

```ts
// localizeColumns.ts — the one rule that turns generated English column
// headers into Hebrew. Kept pure and separate from AppDataGrid because it is
// the only logic here the compiler cannot check, and a fault in it reaches
// every list screen in the application — including the ones already correct.
import type { GridColDef } from '@mui/x-data-grid';
import { fieldLabel } from './fieldLabels';

const HEBREW = /[\u0590-\u05FF]/;

/** True when the text already contains a Hebrew letter. */
export function hasHebrew(text: string | undefined): boolean {
  return text !== undefined && HEBREW.test(text);
}

/**
 * Replace a column's headerName with its Hebrew label.
 *
 * Applied only when we have a label AND the caller did not already supply
 * Hebrew — so a screen that wants a different wording just passes Hebrew and
 * wins. That is what keeps the already-translated screens untouched.
 */
export function localizeColumns<T extends Pick<GridColDef, 'field' | 'headerName'>>(
  columns: T[]
): T[] {
  return columns.map((col) => {
    if (hasHebrew(col.headerName)) return col;
    const label = fieldLabel(col.field);
    return label ? { ...col, headerName: label } : col;
  });
}
```

- [ ] **שלב 4: הרצה לאימות הצלחה**

```bash
npx vitest run src/i18n/localizeColumns.test.ts
```

צפוי: 9 בדיקות עוברות.

- [ ] **שלב 5: commit**

```bash
git add src/i18n/localizeColumns.ts src/i18n/localizeColumns.test.ts
git commit -m "Translate column headers by field name, leaving Hebrew callers alone"
```

---

## Task 4: חיווט `AppDataGrid`

**קבצים:** שינוי `src/components/shared/AppDataGrid.tsx:196,198`

- [ ] **שלב 1: שינוי שם ה-props בפירוק**

בשורה 196 ובשורה 198, להחליף:

```ts
  columns,
```
ב-
```ts
  columns: rawColumns,
```

ובשורה 198:

```ts
  title,
```
ב-
```ts
  title: rawTitle,
```

- [ ] **שלב 2: הוספת הייבוא**

בראש הקובץ, אחרי שאר הייבואים המקומיים:

```ts
import { localizeColumns } from '../../i18n/localizeColumns';
import he from '../../i18n/he';
```

- [ ] **שלב 3: הגדרת המתורגמים בגוף הפונקציה**

מיד אחרי `const storageKey = ...` (שורה 225):

```ts
  // Shadowing the raw props on purpose: every consumer below — the grid, the
  // CSV export, the Excel export, the heading — then picks up the localized
  // version without a separate edit at each site, so none can be missed.
  const columns = useMemo(() => localizeColumns(rawColumns), [rawColumns]);
  const title = useMemo(() => {
    if (rawTitle) return rawTitle;
    const entity = entityName as keyof typeof he.entities;
    return he.entities[entity]?.p;
  }, [rawTitle, entityName]);
```

- [ ] **שלב 4: אימות שאין שימוש שנשאר ב-raw**

```bash
grep -n "rawColumns\|rawTitle" src/components/shared/AppDataGrid.tsx
```

צפוי: בדיוק ארבע שורות — שני הפירוקים ושני ה-`useMemo`.

- [ ] **שלב 5: הידור**

```bash
npx tsc --noEmit
```

צפוי: ללא פלט.

- [ ] **שלב 6: commit**

```bash
git add src/components/shared/AppDataGrid.tsx
git commit -m "Localize grid columns and heading at the shared component"
```

---

## Task 5: `entityLookupCore` (TDD)

**קבצים:** יצירה `src/hooks/entityLookupCore.ts` + `entityLookupCore.test.ts`

- [ ] **שלב 1: כתיבת הבדיקה הנכשלת**

`src/hooks/entityLookupCore.test.ts`:

```ts
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
});
```

- [ ] **שלב 2: הרצה לאימות כישלון**

```bash
npx vitest run src/hooks/entityLookupCore.test.ts
```

צפוי: כישלון — לא ניתן לפתור את הייבוא.

- [ ] **שלב 3: מימוש מינימלי**

`src/hooks/entityLookupCore.ts`:

```ts
// entityLookupCore.ts — pure half of the FK-to-name resolution.
// Separated from the react-query hook so the fallback wording and the Map
// construction can be tested without mounting anything.

export type LookupEntity = 'customer' | 'product' | 'orderHeader';

interface Spec {
  /** Field carrying the human-readable name. */
  display: string;
  /** Noun used to build a label from a bare number. */
  noun: string;
  /**
   * Whether the display value is itself a number needing the noun in front.
   * A customer name stands alone; an order number does not.
   */
  prefixed: boolean;
}

const SPECS: Record<LookupEntity, Spec> = {
  customer: { display: 'customerName', noun: 'לקוח', prefixed: false },
  product: { display: 'productName', noun: 'מוצר', prefixed: false },
  orderHeader: { display: 'orderNumber', noun: 'הזמנה', prefixed: true },
};

/**
 * Label shown when the id cannot be resolved — still loading, or the row was
 * deleted. Deliberately not an empty string: a blank cell reads as a fault,
 * whereas "לקוח #3" reads as data and keeps the id visible.
 */
export function fallbackLabel(entity: LookupEntity, id: number): string {
  return `${SPECS[entity].noun} #${id}`;
}

/**
 * Build an id → name resolver. A Map rather than Array.find per cell: with
 * 1,312 customers and 25 rows the linear scan runs on every render.
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
```

- [ ] **שלב 4: הרצה לאימות הצלחה**

```bash
npx vitest run src/hooks/entityLookupCore.test.ts
```

צפוי: 8 בדיקות עוברות.

- [ ] **שלב 5: commit**

```bash
git add src/hooks/entityLookupCore.ts src/hooks/entityLookupCore.test.ts
git commit -m "Resolve foreign keys to names, falling back to a visible id"
```

---

## Task 6: הוק `useEntityLookup`

**קבצים:** יצירה `src/hooks/useEntityLookup.ts`

- [ ] **שלב 1: כתיבת ההוק**

```ts
// useEntityLookup.ts — id → display name for the three foreign keys in the
// schema. Reuses the query keys the composite order screen already uses, so
// react-query serves this from cache and no extra request is made.
import { useMemo } from 'react';
import { useQuery } from '@tanstack/react-query';
import { CustomerApi } from '../api/CustomerApi';
import { ProductApi } from '../api/ProductApi';
import { OrderHeaderApi } from '../api/OrderHeaderApi';
import { buildLookup, type LookupEntity } from './entityLookupCore';

const SOURCES = {
  customer: { key: ['customers', 'all'], fn: () => CustomerApi.getAll(0, 9999, '') },
  product: { key: ['products', 'all'], fn: () => ProductApi.getAll(0, 9999, '') },
  orderHeader: { key: ['orderHeaders', 'all'], fn: () => OrderHeaderApi.getAll(0, 9999, '') },
} as const;

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
```

> **חתימות ה-API אומתו ב-17.8.2026** — שלושתן זהות:
> `getAll(page = 0, pageSize = 25, search = '', sortField = '', sortDir = 'asc', filters?)`.
> הארגומנט הראשון הוא **מספר עמוד**, לא offset, ולכן `getAll(0, 9999, '')` מחזיר
> את כל הרשומות. זו בדיוק הקריאה ש-`OrderCompositeForm` כבר מבצעת.

- [ ] **שלב 2: הידור**

```bash
npx tsc --noEmit
```

צפוי: ללא פלט.

- [ ] **שלב 3: commit**

```bash
git add src/hooks/useEntityLookup.ts
git commit -m "Add the FK lookup hook, backed by the existing query cache"
```

---

## Task 7: עמודות FK במסכי הרשימה

**קבצים:** שינוי — `CustomerDebtList.tsx:51,61` · `BeehiveBuyerTrackingList.tsx:49` ·
`DeliveryList.tsx:52` · `OrderHeaderList.tsx:56` · `OrderLineList.tsx:49,59` ·
`ProductPriceList.tsx:51` · `SupplierOrderList.tsx:52`

- [ ] **שלב 1: החלת הדפוס בקובץ אחד (`SupplierOrderList.tsx`)**

בראש הרכיב, ליד שאר ההוקים:

```ts
  const orderHeaderName = useEntityLookup('orderHeader');
```

ולהחליף את עמודת ה-FK (שורות 51–60) ב:

```ts
    {
      field: 'fkOrderHeaderId',
      width: 160,
      renderCell: (params) => params.value ? (
        <Link to={`/orderHeaders/${params.value}`} style={{ color: 'inherit', textDecoration: 'underline' }} onClick={(e) => e.stopPropagation()}>
          {orderHeaderName(params.value as number)}
        </Link>
      ) : '',
    },
```

`headerName` הוסר בכוונה — `localizeColumns` יספק אותו מהמילון.

הייבוא:

```ts
import { useEntityLookup } from '../hooks/useEntityLookup';
```

- [ ] **שלב 2: הידור**

```bash
npx tsc --noEmit
```

- [ ] **שלב 3: חזרה על אותו דפוס בשבעת הקבצים הנותרים**

לכל קובץ: להוסיף את ההוק המתאים (`'customer'` עבור `fkCustomerId`,
`'product'` עבור `fkProductId`, `'orderHeader'` עבור `fkOrderHeaderId`),
להסיר `headerName`, ולעטוף את הערך ב-`renderCell` כמו לעיל.

`OrderLineList.tsx` צריך שניים — `product` ו-`orderHeader`.

- [ ] **שלב 4: הידור וקומיט**

```bash
npx tsc --noEmit
git add src/pages
git commit -m "Show the referenced record's name in foreign-key columns"
```

---

## Task 8: מחרוזות מחוץ ל-Grid

**קבצים:** עשרת `src/pages/*List.tsx` המיוצרים

בכל קובץ, להחליף לפי הטבלה. הערכים נלקחים מ-`he.ts` דרך `useTranslation`.

| מחרוזת אנגלית | מקור |
|---|---|
| `Add New` | `t.actions.create` |
| `Quick Create` | `t.actions.quickCreate` |
| `Delete Selected (N)` | `` `${t.actions.bulkDelete} (${n})` `` |
| `Hide Filters` / `Filters` | `t.actions.hideFilters` / `t.actions.filter` |
| `Open` | `t.actions.view` |
| `Edit` / `Delete` / `Duplicate` | `t.actions.edit` / `.delete` / `.duplicate` |
| `Failed to load …` | `t.messages.error` |
| `Are you sure you want to delete …` | `t.messages.confirmDelete` |
| `Copy ID` / `Copy Row` | `'העתק מזהה'` / `'העתק שורה'` |

- [ ] **שלב 1: הוספת ההוק לכל קובץ**

```ts
import useTranslation from '../i18n/useTranslation';
// ובגוף הרכיב:
const { t } = useTranslation();
```

- [ ] **שלב 2: החלפת המחרוזות לפי הטבלה**

- [ ] **שלב 3: תוויות `filterFields` ו-`bulkEditableFields`**

להחליף כל `label: 'English'` ב-`label: fieldLabel('<field>') ?? '<field>'`.
הייבוא: `import { fieldLabel } from '../i18n/fieldLabels';`

- [ ] **שלב 4: אימות**

```bash
grep -n "label: '[A-Z]" src/pages/*List.tsx
```

צפוי: ללא פלט.

> **`headerName` נשאר באנגלית בקוד המקור — וזה מכוון.** `localizeColumns`
> מתרגם אותו בזמן ריצה. היתרון: אם שדה חסר במילון, המשתמש רואה את התווית
> האנגלית ולא כותרת ריקה. לכן **אין** לחפש `headerName` באימות הזה — הימצאותו
> אינה תקלה. מה שנבדק בפועל הוא המסך, ב-Task 11.

- [ ] **שלב 5: הידור וקומיט**

```bash
npx tsc --noEmit
git add src/pages
git commit -m "Translate the list-screen strings the shared grid cannot reach"
```

---

## Task 9: מסכי View ו-Form

**קבצים:** עשרת `*View.tsx` ועשרת `*Form.tsx` המיוצרים

- [ ] **שלב 1: View — תוויות שדות**

בכל `*View.tsx`, כל תווית שדה מוחלפת ב-`fieldLabel('<field>') ?? '<field>'`.
כותרות ו-`aria-label` של כפתורי ההעתקה מתורגמות באותו אופן.

- [ ] **שלב 2: Form — תוויות והודעות**

בכל `*Form.tsx`: `label="English"` ← `label={fieldLabel('<field>')}`,
כפתורים ← `t.actions.*`, הודעות ולידציה ← `t.messages.required`.

- [ ] **שלב 3: אימות**

```bash
grep -n "label=\"[A-Z]" src/pages/*View.tsx src/pages/*Form.tsx
```

צפוי: ללא פלט.

בנוסף, לספור כמה כפתורים באנגלית נותרו:

```bash
grep -c ">Save<\|>Cancel<\|>Delete<\|>Back<" src/pages/*Form.tsx src/pages/*View.tsx | grep -v ":0"
```

צפוי: ללא פלט — כלומר לכל קובץ אפס מופעים.

- [ ] **שלב 4: הידור וקומיט**

```bash
npx tsc --noEmit
git add src/pages
git commit -m "Translate the view and form screens"
```

---

## Task 10: הסרת `addFieldsHere` והסתרת המסך הכפול

**קבצים:** `src/types/ProductPriceHist.ts:19,37,55` ·
`ProductPriceHistList.tsx:85,137,194` · `ProductPriceHistForm.tsx:81,101,439-449` ·
`ProductPriceHistView.tsx:336-345` · `src/routes.ts:47`

- [ ] **שלב 1: הסרת שדה הרפאים**

להסיר כל מופע של `addFieldsHere` משלושת המסכים ומהטיפוס. העמודה אינה קיימת
בטבלה `ProductPriceHist` — אומת מול ה-DB ב-17.8.2026.

- [ ] **שלב 2: אימות שלא נותר מופע**

```bash
grep -rn "addFieldsHere\|Add Fields Here" src/
```

צפוי: ללא פלט.

- [ ] **שלב 3: הסתרת `customerDebts` מהתפריט**

ב-`src/routes.ts:47`:

```ts
  { path: '/customerDebts', label: 'חובות לקוחות', entity: 'customerDebt', group: 'כספים', readOnly: false, hideInNav: true },
```

- [ ] **שלב 4: הידור וקומיט**

```bash
npx tsc --noEmit
git add src/types/ProductPriceHist.ts src/pages src/routes.ts
git commit -m "Drop the AddFieldsHere placeholder and the duplicate debts entry"
```

---

## Task 11: אימות מלא

- [ ] **שלב 1: בדיקות ובנייה**

```bash
npx vitest run
npx tsc --noEmit
cmd /c "npm run build & echo EXIT=%ERRORLEVEL%"
```

צפוי: כל הבדיקות עוברות · `tsc` שקט · `EXIT=0`.

- [ ] **שלב 2: בניית ה-.NET והרמת השרת**

```bash
cd C:/Dev/NonTFS/TargCCOrders
"C:/Program Files/Microsoft Visual Studio/18/Professional/MSBuild/Current/Bin/MSBuild.exe" "TargCCOrders.WebAPIHost/TargCCOrders.WebAPIHost.csproj" -t:Build -v:m -p:Configuration=Debug
```

- [ ] **שלב 3: הרצה ובדיקת בריאות**

```bash
./TargCCOrders.WebAPIHost/bin/Debug/net8.0/TargCCOrders.WebAPIHost.exe --urls http://localhost:5199
```

ובחלון אחר:

```bash
curl.exe -i http://localhost:5199/api/health
```

צפוי: `{"status":"ok","db":"ok"}`

- [ ] **שלב 4: שלוש בדיקות הליבה — לא מדלגים**

| בדיקה | צפוי | מאמת |
|---|---|---|
| מסך לקוחות | 1,312 שורות | סחיפת ordinal |
| הזמנה חדשה | סטטוס **"חדש"** | ה-enum |
| שמירה ופתיחה מחדש | מע"מ ≠ 0 | המע"מ |

- [ ] **שלב 5: בדיקות השינוי עצמו**

| מסך | צפוי |
|---|---|
| שבעת מסכי הרשימה | עברית מלאה, אפס אנגלית |
| עמודות FK | שם אמיתי, לא `לקוח #3` |
| היסטוריית מחירים | אין `Add Fields Here` |
| תפריט כספים | פריט אחד בלבד — `ניהול חובות` |
| ניהול חובות / הזמנות / לוח משלוחים | **ללא רגרסיה** — עדיין עברית תקינה |
| הזמנה עם מוצר בלי מחיר | ההתראה קופצת |

- [ ] **שלב 6: בדיקת נקיון לפני דחיפה**

```bash
git status --short
git status --porcelain | grep -Ei "_token|Database/import/|IMPORT_2_migrate|\.csv$"
```

צפוי: ה-grep השני **ללא פלט**. אם הוא מחזיר משהו — לעצור, הקבצים האלה מכילים
1,433 לקוחות אמיתיים ואסור שיגיעו לגיט.

> **לעולם לא `git add -A` בפרויקט הזה.** מוסיפים קבצים בשמם המפורש.

- [ ] **שלב 7: דחיפה — רק אחרי אישור**

הדחיפה אינה אוטומטית. להציג למשתמש את `git log --oneline` ואת תוצאות האימות
החי, ולדחוף רק לאחר אישור מפורש:

```bash
git push origin main
```

- [ ] **שלב 8: Publish לשרת**

מחוץ להיקף התוכנית הזו. המחזור המלא מתועד ב-`DEPLOY_TO_SERVER.md`, וכולל את
שורת ה-`Copy-Item` של `dist` ל-`wwwroot` שבלעדיה השרת מגיש ממשק ישן.

---

## הערות אימות

**רגרסיה היא הסיכון, לא הפיצ'ר.** `AppDataGrid` משרת כל מסך רשימה. השורה
החשובה בטבלה שלמעלה היא זו שבודקת ש-`ניהול חובות`, `הזמנות` ו`לוח משלוחים` —
שעובדים היום — לא נשברו. אם מדלגים על משהו, לא מדלגים על זה.

**הידור נקי אינו הוכחה.** שני הבאגים הגרועים בפרויקט (`System.Management`,
סחיפת ordinal) עברו הידור מושלם ונכשלו בזמן ריצה.

**אין להריץ TargCC** בשום שלב.
