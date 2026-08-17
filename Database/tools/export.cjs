// Export the three master-data sheets to UTF-8 CSV for staging load.
// Straight dump: no cleaning here, so the raw values stay inspectable.
const fs = require('fs');
const { ExcelJS, FILE, cell, findHeaderRow } = require('./_xlhelp.cjs');

const OUT = 'C:\\Dev\\NonTFS\\TargCCOrders\\Database\\import\\';
const SHEETS = {
  'לקוחות': 'customers.csv',
  'מחירון': 'pricelist.csv',
  'קוני כוורות': 'beehives.csv',
};

const q = (s) => '"' + String(s).replace(/"/g, '""').replace(/\r?\n/g, ' ') + '"';

(async () => {
  fs.mkdirSync(OUT, { recursive: true });
  const wb = new ExcelJS.Workbook();
  await wb.xlsx.readFile(FILE);

  for (const [sheet, file] of Object.entries(SHEETS)) {
    const ws = wb.getWorksheet(sheet);
    const hr = findHeaderRow(ws);
    const width = ws.columnCount;
    const lines = [];
    // Positional headers: the Hebrew labels are inconsistent and some are blank,
    // so columns are named c1..cN and mapped explicitly in SQL.
    lines.push(Array.from({ length: width }, (_, i) => 'c' + (i + 1)).join(','));

    let n = 0;
    for (let r = hr + 1; r <= ws.rowCount; r++) {
      const vals = ws.getRow(r).values;
      if (!Array.isArray(vals)) continue;
      const cells = [];
      for (let i = 1; i <= width; i++) cells.push(cell(vals[i]));
      if (cells.filter(c => c !== '').length === 0) continue;
      lines.push(cells.map(q).join(','));
      n++;
    }
    fs.writeFileSync(OUT + file, '\uFEFF' + lines.join('\r\n'), 'utf8');
    console.log(`${sheet} -> ${file}  rows=${n}  cols=${width}  headerRow=${hr}`);
  }
})().catch(e => console.error('ERR ' + e.stack));
