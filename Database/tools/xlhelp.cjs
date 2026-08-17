// Deep survey of the three sheets worth importing. Read-only.
// Finds the real header row (not always row 1), counts usable rows, and
// reports data-quality problems before any SQL is written.
const ExcelJS = require('exceljs');
const FILE = 'C:\\Dev\\NonTFS\\TargCCOrders\\גיבוי של מערכת תיפעולית -הזמנות 2022 (29_5_2023).xlsx';

const cell = (v) => {
  if (v === null || v === undefined) return '';
  if (typeof v === 'object') {
    if (v.text !== undefined) return String(v.text).trim();
    if (v.result !== undefined) return String(v.result).trim();
    if (v.richText) return v.richText.map(r => r.text).join('').trim();
    if (v instanceof Date) return v.toISOString().slice(0, 10);
    return '';
  }
  return String(v).trim();
};

function findHeaderRow(ws, maxScan = 12) {
  let best = { row: 1, filled: -1 };
  for (let r = 1; r <= Math.min(maxScan, ws.rowCount); r++) {
    const vals = ws.getRow(r).values;
    if (!Array.isArray(vals)) continue;
    const filled = vals.slice(1).filter(v => cell(v) !== '').length;
    if (filled > best.filled) best = { row: r, filled };
  }
  return best.row;
}

module.exports = { ExcelJS, FILE, cell, findHeaderRow };
