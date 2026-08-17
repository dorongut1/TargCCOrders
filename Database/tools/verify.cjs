const { execSync } = require('child_process');
// Verification via node so the Hebrew is readable (sqlcmd's console mangles it).
const q = (sql) => {
  const f = require('os').tmpdir() + '\\_v.sql';
  require('fs').writeFileSync(f, sql, 'utf8');
  return execSync(`sqlcmd -S Localhost -d TargCCOrdersNew -E -I -f 65001 -s "|" -W -i "${f}"`,
    { encoding: 'utf8' });
};

console.log('=== SANITY: farmer price for פרסמיליס (expect 52) ===');
console.log(q(`SET NOCOUNT ON;
SELECT p.ProductCode, p.ProductName, p.BaseCost, pp.enmCustomerType, pp.SellingPrice
FROM dbo.Product p JOIN dbo.ProductPrice pp ON pp.ProductID = p.ID
WHERE p.ProductCode = '1020' ORDER BY pp.enmCustomerType;`));

console.log('=== customer type spread ===');
console.log(q(`SET NOCOUNT ON;
SELECT enmCustomerType, COUNT(*) FROM dbo.Customer GROUP BY enmCustomerType ORDER BY COUNT(*) DESC;`));

console.log('=== sample customers ===');
console.log(q(`SET NOCOUNT ON;
SELECT TOP 5 CustomerCode, CustomerName, City, enmCustomerType, Phone
FROM dbo.Customer WHERE AddedBy='ExcelImport' ORDER BY ID DESC;`));

console.log('=== products without any price (should be small) ===');
console.log(q(`SET NOCOUNT ON;
SELECT COUNT(*) FROM dbo.Product p WHERE NOT EXISTS (SELECT 1 FROM dbo.ProductPrice pp WHERE pp.ProductID=p.ID);`));
