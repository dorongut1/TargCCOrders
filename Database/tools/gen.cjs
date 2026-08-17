const { fs, readCsv, OUT, N, D, I, CUST_TYPE, CATEGORY, PRICE_COLS } = require('./_genhelp.cjs');

const L = [];
L.push(`/* ============================================================================
   Master-data import from the 2022 workbook — MIGRATION (step 2 of 2)
   ----------------------------------------------------------------------------
   Generated ${new Date().toISOString().slice(0, 16)} by _gen.cjs from the staged CSVs.
   Do not hand-edit: regenerate instead.

   Everything runs inside ONE transaction. Any error rolls the whole thing back,
   so the database is never left half-imported.

   MERGE, not truncate: rows are matched on ProductCode / CustomerCode. Existing
   rows are updated, new ones inserted. Deleting and reloading would break the
   foreign keys of the orders already in the system.

   Rows the workbook could not supply cleanly are skipped, not guessed:
     - customer code non-numeric or blank        (118 rows)
     - customer name blank                       (2 rows)
     - product code non-numeric or name blank    (29 rows)
     - reminder month outside 1..12              (9 rows, e.g. "99", "27.11")
   Prices are only written where the sheet held a positive number.

   Run with: sqlcmd -I -f 65001
   ============================================================================ */

USE TargCCOrdersNew;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;
GO
`);

// ---------- products ----------
const price = readCsv('pricelist.csv');
let nProd = 0, nPrice = 0, skippedProd = 0;

L.push(`PRINT '--- products ---';`);
for (const r of price) {
  const code = (r[0] || '').trim();
  const name = (r[1] || '').trim();
  if (!/^\d+$/.test(code) || !name) { skippedProd++; continue; }
  const cost = D(r[2]);
  const cat = CATEGORY[(r[8] || '').trim()] || 'General';
  nProd++;
  L.push(`
MERGE dbo.Product AS t
USING (SELECT ${N(code)} AS ProductCode) AS s ON t.ProductCode = s.ProductCode
WHEN MATCHED THEN UPDATE SET ProductName=${N(name)}, enmCategory=${N(cat)},
     BaseCost=${cost === null ? 't.BaseCost' : cost}, blg_IsActive=1,
     ChangedBy=N'ExcelImport', ChangedOn=GETDATE()
WHEN NOT MATCHED THEN INSERT (ProductCode, ProductName, enmCategory, BaseCost, blg_IsActive, AddedBy, AddedOn)
     VALUES (${N(code)}, ${N(name)}, ${N(cat)}, ${cost === null ? 'NULL' : cost}, 1, N'ExcelImport', GETDATE());`);
}

// ---------- prices ----------
L.push(`\nPRINT '--- prices ---';`);
for (const r of price) {
  const code = (r[0] || '').trim();
  const name = (r[1] || '').trim();
  if (!/^\d+$/.test(code) || !name) continue;
  const minQty = I(r[6]);
  for (const pc of PRICE_COLS) {
    const v = D(r[pc.idx]);
    if (v === null || v <= 0) continue;
    nPrice++;
    L.push(`
MERGE dbo.ProductPrice AS t
USING (SELECT p.ID AS ProductID, ${N(pc.type)} AS enmCustomerType
       FROM dbo.Product p WHERE p.ProductCode = ${N(code)}) AS s
  ON t.ProductID = s.ProductID AND t.enmCustomerType = s.enmCustomerType
WHEN MATCHED THEN UPDATE SET SellingPrice=${v}, MinQuantity=${minQty === null ? 't.MinQuantity' : minQty},
     ChangedBy=N'ExcelImport', ChangedOn=GETDATE()
WHEN NOT MATCHED THEN INSERT (ProductID, enmCustomerType, SellingPrice, MinQuantity, AddedBy, AddedOn)
     VALUES (s.ProductID, s.enmCustomerType, ${v}, ${minQty === null ? 'NULL' : minQty}, N'ExcelImport', GETDATE());`);
  }
}

fs.writeFileSync(OUT, L.join('\n'), 'utf8');
console.log(`products=${nProd} skipped=${skippedProd} priceRows=${nPrice}`);
console.log('part 1 written');

// ---------- customers ----------
const cust = readCsv('customers.csv');
const seen = new Set();
let nCust = 0, skippedCust = 0;
const C = [];

C.push(`\nPRINT '--- customers ---';`);
for (const r of cust) {
  const code = (r[1] || '').trim();
  const name = (r[2] || '').trim();
  if (!/^\d+$/.test(code) || !name) { skippedCust++; continue; }
  if (seen.has(code)) { skippedCust++; continue; }   // first row wins on duplicates
  seen.add(code);

  const rawPhone = (r[3] || '').trim();
  // Phones sometimes carry a contact name: "יואב 0524475843". Keep the digits.
  const phoneMatch = rawPhone.match(/[\d\-+() ]{7,}/);
  const phone = phoneMatch ? phoneMatch[0].trim() : rawPhone;

  const type = CUST_TYPE[(r[11] || '').trim()] || null;
  const terms = I(r[12]);
  nCust++;
  C.push(`
MERGE dbo.Customer AS t
USING (SELECT ${N(code)} AS CustomerCode) AS s ON t.CustomerCode = s.CustomerCode
WHEN MATCHED THEN UPDATE SET CustomerName=${N(name)}, Phone=${N(phone)},
     Email=${N((r[8] || '').trim())}, Address=${N((r[7] || '').trim())},
     City=${N((r[4] || '').trim())}, TaxID=${N((r[6] || '').trim())},
     InvoiceName=${N((r[5] || '').trim())},
     ${type ? `enmCustomerType=${N(type)},` : ''}
     PaymentTermsDays=${terms === null ? 't.PaymentTermsDays' : terms},
     Notes=${N((r[13] || '').trim())}, RivhitCustomerNo=${I(code)},
     blg_IsActive=1, ChangedBy=N'ExcelImport', ChangedOn=GETDATE()
WHEN NOT MATCHED THEN INSERT (CustomerCode, CustomerName, Phone, Email, Address, City,
     TaxID, InvoiceName, enmCustomerType, PaymentTermsDays, Notes, RivhitCustomerNo,
     blg_IsActive, AddedBy, AddedOn)
     VALUES (${N(code)}, ${N(name)}, ${N(phone)}, ${N((r[8] || '').trim())},
     ${N((r[7] || '').trim())}, ${N((r[4] || '').trim())}, ${N((r[6] || '').trim())},
     ${N((r[5] || '').trim())}, ${N(type || 'Private')}, ${terms === null ? 'NULL' : terms},
     ${N((r[13] || '').trim())}, ${I(code)}, 1, N'ExcelImport', GETDATE());`);
}

fs.appendFileSync(OUT, C.join('\n'), 'utf8');
console.log(`customers=${nCust} skipped=${skippedCust}`);

// ---------- beehive tracking ----------
// Linked to the customer by the sheet's composite key "1102 אביב איתן":
// the leading number is the customer code.
const hive = readCsv('beehives.csv');
let nHive = 0, skippedHive = 0;
const H = [];

H.push(`\nPRINT '--- beehive tracking ---';`);
for (const r of hive) {
  const key = (r[0] || '').trim();
  const m = key.match(/^(\d+)\s/);
  if (!m) { skippedHive++; continue; }
  const code = m[1];

  const qty = I(r[2]);
  if (qty === null || qty <= 0) { skippedHive++; continue; }  // no hives, nothing to track

  // Reminder month must be 1..12; the sheet also holds "99", "27.11" and free text.
  const rawMonth = (r[5] || '').trim();
  const month = /^([1-9]|1[0-2])$/.test(rawMonth) ? parseInt(rawMonth, 10) : null;

  let lastDate = 'NULL';
  const d = new Date(r[1]);
  if (r[1] && !isNaN(d.getTime()) && d.getFullYear() > 1990) {
    lastDate = `'${d.toISOString().slice(0, 10)}'`;
  }

  nHive++;
  H.push(`
MERGE dbo.BeehiveBuyerTracking AS t
USING (SELECT c.ID AS CustomerID FROM dbo.Customer c WHERE c.CustomerCode = ${N(code)}) AS s
  ON t.CustomerID = s.CustomerID
WHEN MATCHED THEN UPDATE SET BeehiveQuantity=${qty}, LastOrderDate=${lastDate},
     ReminderMonth=${month === null ? 'NULL' : month}, Notes=${N((r[4] || '').trim())},
     blg_IsRelevant=1, ChangedBy=N'ExcelImport', ChangedOn=GETDATE()
WHEN NOT MATCHED THEN INSERT (CustomerID, LastOrderDate, BeehiveQuantity, ReminderMonth,
     Notes, blg_IsRelevant, AddedBy, AddedOn)
     VALUES (s.CustomerID, ${lastDate}, ${qty}, ${month === null ? 'NULL' : month},
     ${N((r[4] || '').trim())}, 1, N'ExcelImport', GETDATE());`);
}

H.push(`
GO
COMMIT TRANSACTION;
GO
PRINT '=== import complete ===';
SELECT 'Product' AS t, COUNT(*) AS n FROM dbo.Product
UNION ALL SELECT 'ProductPrice', COUNT(*) FROM dbo.ProductPrice
UNION ALL SELECT 'Customer', COUNT(*) FROM dbo.Customer
UNION ALL SELECT 'BeehiveBuyerTracking', COUNT(*) FROM dbo.BeehiveBuyerTracking;
GO`);

fs.appendFileSync(OUT, H.join('\n'), 'utf8');
console.log(`beehives=${nHive} skipped=${skippedHive}`);
console.log('DONE -> ' + OUT);
