// resolveForeignKeys.tsx — show the referenced record's name in foreign-key
// columns.
//
// Lives beside localizeColumns because it finishes the same job: that one
// makes the header read "שם לקוח", this one makes the cell read "אבי אלון"
// rather than "3". A header naming a thing above a column of raw ids is worse
// than leaving both in English, so the two belong together.
//
// Safe to apply wholesale because AppDataGrid is used only by the generated
// screens — every hand-written Hebrew screen renders a DataGrid directly and
// never passes through here.
import { Link } from 'react-router-dom';
import type { GridColDef } from '@mui/x-data-grid';
import type { LookupEntity } from '../hooks/entityLookupCore';

interface ForeignKey {
  entity: LookupEntity;
  /** Route prefix for the referenced record. */
  path: string;
}

/** The only three foreign keys in the schema. */
export const FK_FIELDS: Record<string, ForeignKey> = {
  fkCustomerId: { entity: 'customer', path: '/customers' },
  fkProductId: { entity: 'product', path: '/products' },
  fkOrderHeaderId: { entity: 'orderHeader', path: '/orderHeaders' },
};

/** Which lookups a column set actually needs, so the rest are never fetched. */
export function neededLookups(columns: Pick<GridColDef, 'field'>[]): Set<LookupEntity> {
  const needed = new Set<LookupEntity>();
  for (const col of columns) {
    const fk = FK_FIELDS[col.field];
    if (fk) needed.add(fk.entity);
  }
  return needed;
}

/**
 * Replace each foreign-key column's cell with a link showing the resolved name.
 *
 * The generated renderCell is deliberately overwritten: it links to the right
 * record but labels the link with the id, which is the whole complaint. The
 * link itself is rebuilt from the raw row value rather than from the rendered
 * one, so the destination stays correct once the text is a name.
 */
export function resolveForeignKeys<T extends GridColDef>(
  columns: T[],
  lookup: (entity: LookupEntity) => (id: number) => string
): T[] {
  return columns.map((col) => {
    const fk = FK_FIELDS[col.field];
    if (!fk) return col;
    return {
      ...col,
      renderCell: (params: { value?: unknown }) => {
        const id = params.value;
        if (id == null || id === '') return '';
        return (
          <Link
            to={`${fk.path}/${id}`}
            style={{ color: 'inherit', textDecoration: 'underline' }}
            onClick={(e) => e.stopPropagation()}
          >
            {lookup(fk.entity)(Number(id))}
          </Link>
        );
      },
    };
  });
}
