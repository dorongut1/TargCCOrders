// One-off survey of the legacy workbook. Read-only: it prints structure and a
// few sample rows so we can judge what is worth importing before writing any SQL.
const ExcelJS = require('exceljs');
const path = 'C:\\Dev\\NonTFS\\TargCCOrders\\גיבוי של מערכת תיפעולית -הזמנות 2022 (29_5_2023).xlsx';

(async () => {
  const wb = new ExcelJS.Workbook();
  await wb.xlsx.readFile(path);
  console.log('SHEETS: ' + wb.worksheets.length);
  wb.worksheets.forEach(ws => {
    console.log('---');
    console.log('NAME : ' + ws.name);
    console.log('ROWS : ' + ws.rowCount + '  COLS: ' + ws.columnCount);
    const hdr = ws.getRow(1).values;
    if (Array.isArray(hdr)) {
      console.log('HDR  : ' + hdr.slice(1, 30).map(v => (v && v.text) ? v.text : v).join(' | '));
    }
  });
})().catch(e => console.error('ERR ' + e.message));
