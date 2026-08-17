// Generates the migration SQL from the staged CSVs.
// Written by node rather than hand-authored so the Hebrew survives, and so the
// row filtering is explicit and auditable.
const fs = require('fs');
const { readCsv } = require('./_csv.cjs');

const OUT = 'C:\\Dev\\NonTFS\\TargCCOrders\\Database\\IMPORT_2_migrate_2026-08-17.sql';

const N = (s) => (s === null || s === undefined || s === '') ? 'NULL' : "N'" + String(s).replace(/'/g, "''") + "'";
const D = (v) => { const n = parseFloat(String(v).replace(/[^\d.\-]/g, '')); return isFinite(n) ? n : null; };
const I = (v) => { const n = parseInt(String(v).replace(/[^\d\-]/g, ''), 10); return isFinite(n) ? n : null; };

// Sheet label -> enum member stored in the DB (English; locText carries Hebrew).
const CUST_TYPE = {
  'פרטי': 'Private', 'חקלאים': 'Farmer', 'חוות': 'Farm',
  'קמעוני': 'Retail', 'הידרו': 'Hydro',
};
const CATEGORY = {
  'הדברה ביולוגית': 'BiologicalPest', 'ביותים': 'Biotime', 'שמוליק': 'Shmoolik',
  'כוורות': 'Beehives', 'קאנרייז': 'Canrise', 'בוטנו': 'Butano',
  'ביולייף': 'Biolife', 'משלוח': 'Delivery',
};
// Price columns c4/c5/c6 -> the customer class each one applies to.
const PRICE_COLS = [
  { idx: 3, type: 'Private' },
  { idx: 4, type: 'Farmer' },
  { idx: 5, type: 'Hydro' },
];

module.exports = { fs, readCsv, OUT, N, D, I, CUST_TYPE, CATEGORY, PRICE_COLS };
