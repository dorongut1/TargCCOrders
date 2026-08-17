// Repairs ordinal drift in the Customer SELECT procedures.
//
// ROOT CAUSE — identical to the VATRatePercent incident of 2026-08-09:
// RivhitCustomerNo and enmDefaultDeliveryMethod were added to the Customer table
// and inserted into the procedures' SELECT lists BEFORE the auditing column,
// but the generated VB reads by ORDINAL and still expects AddedOn at index 18:
//     If Not vReader.IsDBNull(18) Then bDateAdded = vReader.GetDateTime(18)
// While those two columns were NULL for every row, IsDBNull(18) was true and the
// cast never ran, so the fault stayed dormant. Importing real Rivhit numbers made
// index 18 an Int32 and every customer read began failing with
//     "Unable to cast object of type 'System.Int32' to type 'System.DateTime'".
//
// FIX: move the two late-added columns AFTER the auditing column so AddedOn
// returns to index 18. The procedures are corrected rather than the VB, because
// the VB is generated and would be overwritten.
const { execSync } = require('child_process');
const fs = require('fs');
const os = require('os');

const run = (sql) => {
  const f = os.tmpdir() + '\\_fix.sql';
  fs.writeFileSync(f, sql, 'utf8');
  return execSync(`sqlcmd -S Localhost -d TargCCOrdersNew -E -I -f 65001 -b -i "${f}"`, { encoding: 'utf8' });
};

const getDef = (name) => {
  const f = os.tmpdir() + '\\_get.sql';
  fs.writeFileSync(f, `SET NOCOUNT ON;\nSELECT OBJECT_DEFINITION(OBJECT_ID('dbo.${name}'));`, 'utf8');
  const out = execSync(`sqlcmd -S Localhost -d TargCCOrdersNew -E -I -f 65001 -y 0 -i "${f}"`, { encoding: 'utf8' });
  return out;
};

module.exports = { run, getDef };
