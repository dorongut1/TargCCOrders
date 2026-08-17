const { execSync } = require('child_process');
const q = (sql) => {
  const f = require('os').tmpdir() + '\\_v2.sql';
  require('fs').writeFileSync(f, sql, 'utf8');
  return execSync(`sqlcmd -S Localhost -d TargCCOrdersNew -E -I -f 65001 -s "|" -W -i "${f}"`, { encoding: 'utf8' });
};

console.log('=== ProductPrice: types NOT matching the enum (legacy Hebrew rows) ===');
console.log(q(`SET NOCOUNT ON;
SELECT pp.enmCustomerType, COUNT(*) AS n, MIN(pp.AddedBy) AS addedBy
FROM dbo.ProductPrice pp GROUP BY pp.enmCustomerType ORDER BY n DESC;`));

console.log('=== Customer: same check ===');
console.log(q(`SET NOCOUNT ON;
SELECT enmCustomerType, COUNT(*) AS n FROM dbo.Customer GROUP BY enmCustomerType;`));

console.log('=== products with no price ===');
console.log(q(`SET NOCOUNT ON;
SELECT TOP 20 p.ProductCode, p.ProductName, p.AddedBy
FROM dbo.Product p WHERE NOT EXISTS (SELECT 1 FROM dbo.ProductPrice pp WHERE pp.ProductID=p.ID);`));
