const { run, getDef } = require('./_fixhelp.cjs');
const { execSync } = require('child_process');
const fs = require('fs'), os = require('os');

// CustomerDebt drift: the VB reads
//   5 clc_RemainingAmount, 6 DebtDate, 7 DueDate, 12 DeliveryDate, 13 AddedOn
// but the procedures emit clc_RemainingAmount last, at 12, pushing DebtDate to 5.
// Moving it back to just after PaidAmount restores every following index.
const f = os.tmpdir() + '\\_list.sql';
fs.writeFileSync(f, `SET NOCOUNT ON;
SELECT name FROM sys.procedures
WHERE OBJECT_DEFINITION(object_id) LIKE '%[[]clc_RemainingAmount]%'
  AND OBJECT_DEFINITION(object_id) LIKE '%SELECT%'
  AND name NOT LIKE '%Update%' AND name NOT LIKE '%Delete%'
ORDER BY name;`, 'utf8');
const names = execSync(`sqlcmd -S Localhost -d TargCCOrdersNew -E -I -f 65001 -y 0 -i "${f}"`, { encoding: 'utf8' })
  .split(/\r?\n/).map(s => s.trim())
  .filter(s => s && !/^-+$/.test(s) && !/^name$/i.test(s) && !/rows affected/i.test(s));

const MOVE = /\s*,\s*\[CustomerDebt\]\.\[clc_RemainingAmount\]/g;
let fixed = [], skipped = [];

for (const p of names) {
  let def = getDef(p).replace(/^\s*\r?\n/, '').trimEnd();
  if (!MOVE.test(def)) { skipped.push(p); continue; }
  MOVE.lastIndex = 0;

  let out = def.replace(MOVE, '');
  const anchor = /(,\s*\[CustomerDebt\]\.\[PaidAmount\])/g;
  if (!anchor.test(out)) { skipped.push(p + ' (no PaidAmount)'); continue; }
  anchor.lastIndex = 0;
  out = out.replace(anchor, '$1\n              , [CustomerDebt].[clc_RemainingAmount]');
  out = out.replace(/^\s*CREATE\s+PROCEDURE/i, 'CREATE OR ALTER PROCEDURE');

  try { run(out); fixed.push(p); }
  catch (e) { skipped.push(p + ' ERR:' + String(e.message).slice(0, 80)); }
}
console.log('FIXED  (' + fixed.length + '): ' + fixed.join(', '));
console.log('SKIPPED(' + skipped.length + '): ' + skipped.join(', '));
