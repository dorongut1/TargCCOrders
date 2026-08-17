// localizeColumns.ts — the one rule that turns generated English column
// headers into Hebrew.
//
// Kept pure and separate from AppDataGrid because it is the only logic in this
// change the compiler cannot check, and because it runs for every list screen
// in the application — including the ones that are already correct.
import type { GridColDef } from '@mui/x-data-grid';
import { fieldLabel } from './fieldLabels';

// Unicode Hebrew block. Written as code points rather than a regex over
// literal characters: the range endpoints are unassigned code points, so in
// source they are invisible and an editor can silently mangle them.
const HEBREW_FIRST = 0x0590;
const HEBREW_LAST = 0x05ff;

/** True when the text already contains a Hebrew character. */
export function hasHebrew(text: string | undefined): boolean {
  if (text === undefined) return false;
  for (const char of text) {
    const cp = char.codePointAt(0);
    if (cp !== undefined && cp >= HEBREW_FIRST && cp <= HEBREW_LAST) return true;
  }
  return false;
}

/**
 * Replace each column's headerName with its Hebrew label.
 *
 * Applied only when we have a label AND the caller did not already supply
 * Hebrew, so a screen that wants different wording just passes Hebrew and
 * wins. That is what leaves the hand-written screens untouched.
 *
 * A column whose field we don't know keeps whatever it had: an English header
 * is a worse result than a Hebrew one, but a better result than a blank.
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
