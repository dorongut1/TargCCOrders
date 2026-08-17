const { ExcelJS, FILE, cell, findHeaderRow } = require('./_xlhelp.cjs');

(async () => {
  const wb = new ExcelJS.Workbook();
  await wb.xlsx.readFile(FILE);

  for (const name of ['לקוחות', 'מחירון', 'קוני כוורות']) {
    const ws = wb.getWorksheet(name);
    if (!ws) { console.log(`### ${name}: NOT FOUND`); continue; }
    const hr = findHeaderRow(ws);
    const hdr = ws.getRow(hr).values.slice(1).map(cell);

    console.log(`\n### SHEET: ${name}   headerRow=${hr}  declaredRows=${ws.rowCount}`);
    hdr.forEach((h, i) => { if (h) console.log(`   col ${i + 1}: ${h}`); });

    let nonEmpty = 0;
    const samples = [];
    for (let r = hr + 1; r <= ws.rowCount; r++) {
      const vals = ws.getRow(r).values;
      if (!Array.isArray(vals)) continue;
      const cells = vals.slice(1).map(cell);
      if (cells.filter(c => c !== '').length === 0) continue;
      nonEmpty++;
      if (samples.length < 3) samples.push(cells.slice(0, 15).join(' | '));
    }
    console.log(`   -> non-empty data rows: ${nonEmpty}`);
    samples.forEach((s, i) => console.log(`   sample${i + 1}: ${s}`));
  }
})().catch(e => console.error('ERR ' + e.stack));
