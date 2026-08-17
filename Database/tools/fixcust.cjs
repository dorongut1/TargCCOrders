const { run, getDef } = require('./_fixhelp.cjs');
const fs = require('fs');

// Only the SELECT-shaped procedures matter: Update/Delete procs take the columns
// as parameters and are unaffected by SELECT ordering.
const PROCS = [
  'ccCustomerGetByCustomerCode', 'ccCustomerGetByID', 'ccCustomerGetByRivhitCustomerNo',
  'ccCustomersFill', 'ccCustomersFillByBoundedCustomerCode', 'ccCustomersFillByBoundedID',
  'ccCustomersFillByBoundedRivhitCustomerNo', 'ccCustomersFillByCustomerType',
  'ccCustomersFillByWildCardCustomerCode',
];

const LATE = /\s*,\s*\[Customer\]\.\[RivhitCustomerNo\]\s*,\s*\[Customer\]\.\[enmDefaultDeliveryMethod\]/g;

let fixed = [], skipped = [];
for (const p of PROCS) {
  let def = getDef(p);
  def = def.replace(/^\s*\r?\n/, '').trimEnd();
  if (!LATE.test(def)) { skipped.push(p + ' (pattern not found)'); continue; }
  LATE.lastIndex = 0;

  // Drop the pair from its current position...
  let out = def.replace(LATE, '');
  // ...and re-insert it after the auditing column.
  const auditRe = /(,\s*\[Customer\]\.\[AddedOn\])/;
  if (!auditRe.test(out)) { skipped.push(p + ' (no AddedOn)'); continue; }
  out = out.replace(auditRe,
    '$1\n        , [Customer].[RivhitCustomerNo]\n        , [Customer].[enmDefaultDeliveryMethod]');

  out = out.replace(/^\s*CREATE\s+PROCEDURE/i, 'CREATE OR ALTER PROCEDURE');
  fs.writeFileSync('C:\\Dev\\NonTFS\\TargCCOrders\\Database\\_tmp_proc.sql', out, 'utf8');
  run(out);
  fixed.push(p);
}

console.log('FIXED  (' + fixed.length + '): ' + fixed.join(', '));
console.log('SKIPPED(' + skipped.length + '): ' + skipped.join(', '));
