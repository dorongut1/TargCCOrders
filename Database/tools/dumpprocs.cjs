// Captures the repaired procedure definitions into a single replayable script,
// so the fix exists in source control and not only inside the database.
const { getDef } = require('./_fixhelp.cjs');
const fs = require('fs');

const OUT = 'C:\\Dev\\NonTFS\\TargCCOrders\\Database\\FIX_ProcOrdinalDrift_2026-08-17.sql';
const PROCS = [
  'ccCustomerGetByCustomerCode', 'ccCustomerGetByID', 'ccCustomerGetByRivhitCustomerNo',
  'ccCustomersFill', 'ccCustomersFillByBoundedCustomerCode', 'ccCustomersFillByBoundedID',
  'ccCustomersFillByBoundedRivhitCustomerNo', 'ccCustomersFillByCustomerType',
  'ccCustomersFillByWildCardCustomerCode',
  'ccCustomerDebtGetByID', 'ccCustomerDebtsFill', 'ccCustomerDebtsFillByBoundedID',
  'ccCustomerDebtsFillByCustomerID', 'ccCustomerDebtsFillByDebtStatus',
  'ccCustomerDebtsFillByOrderHeaderID', 'ccCustomerDebtsFillOnTheFly',
];

const HEAD = `/* ============================================================================
   FIX: column-ordinal drift in the Customer and CustomerDebt SELECT procedures
   ----------------------------------------------------------------------------
   The generated VB reads result columns BY POSITION. When columns were added to
   these tables the procedures' SELECT lists were updated but the VB was not
   regenerated, so the positions no longer agree:

     Customer      VB expects AddedOn at 18; the procedure returned
                   RivhitCustomerNo (int) there
                   -> "Unable to cast object of type 'System.Int32' to
                       type 'System.DateTime'"
     CustomerDebt  VB expects clc_RemainingAmount at 5; the procedure returned
                   it last, at 12, shifting DebtDate/DueDate/DeliveryDate
                   -> "Unable to cast object of type 'System.DateTime' to
                       type 'System.Decimal'"

   Both faults were DORMANT while the affected columns were NULL for every row,
   because the generated code guards each read with IsDBNull. Importing the real
   customer data populated RivhitCustomerNo and both endpoints began failing.
   This is the same failure mode as the VATRatePercent incident of 2026-08-09.

   The procedures are corrected rather than the VB, because the VB is generated
   and would be overwritten. Re-running TargCC WILL regenerate these procedures
   and reintroduce the drift — re-apply this script afterwards.

   Generated ${new Date().toISOString().slice(0, 16)}. Run with: sqlcmd -I -f 65001
   ============================================================================ */

USE TargCCOrdersNew;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO
`;

const parts = [HEAD];
for (const p of PROCS) {
  let def = getDef(p).replace(/^\s*\r?\n/, '').trimEnd();
  def = def.replace(/^\s*CREATE\s+PROCEDURE/i, 'CREATE OR ALTER PROCEDURE');
  parts.push(`\n/* ---- ${p} ---- */\n${def}\nGO\n`);
}
parts.push(`\nPRINT 'FIX_ProcOrdinalDrift_2026-08-17 applied (${PROCS.length} procedures).';\nGO\n`);

fs.writeFileSync(OUT, parts.join('\n'), 'utf8');
console.log('written: ' + OUT + '  procs=' + PROCS.length);
