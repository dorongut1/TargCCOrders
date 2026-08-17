const { readCsv, tally } = require('./_csv.cjs');

const cust = readCsv('customers.csv');
const price = readCsv('pricelist.csv');
const hive = readCsv('beehives.csv');

console.log('CUSTOMERS rows=' + cust.length);
console.log('  c12 customer type:');
tally(cust, 11).forEach(([v, n]) => console.log(`    ${n,6}  ${v}`));
console.log('  c11 (mostly empty?):  non-empty=' +
  cust.filter(r => (r[10] || '').trim()).length);

const badCode = cust.filter(r => !/^\d+$/.test((r[1] || '').trim()));
const noName = cust.filter(r => !(r[2] || '').trim());
console.log('  bad/blank customer code: ' + badCode.length);
console.log('  blank name             : ' + noName.length);

const codes = new Map();
cust.forEach(r => { const k = (r[1] || '').trim(); if (/^\d+$/.test(k)) codes.set(k, (codes.get(k) || 0) + 1); });
console.log('  duplicate codes        : ' + [...codes.values()].filter(v => v > 1).length);

console.log('\nPRICELIST rows=' + price.length);
console.log('  categories:');
tally(price, 8).forEach(([v, n]) => console.log(`    ${n,6}  ${v}`));
const badP = price.filter(r => !/^\d+$/.test((r[0] || '').trim()) || !(r[1] || '').trim());
console.log('  bad code or blank name : ' + badP.length);
let priceRows = 0;
price.forEach(r => [3, 4, 5].forEach(i => { if (parseFloat(r[i]) > 0) priceRows++; }));
console.log('  price rows that will be created: ' + priceRows);

console.log('\nBEEHIVES rows=' + hive.length);
const withQty = hive.filter(r => parseFloat(r[2]) > 0);
console.log('  with hive quantity > 0 : ' + withQty.length);
const badMonth = hive.filter(r => (r[5] || '').trim() && !/^([1-9]|1[0-2])$/.test((r[5] || '').trim()));
console.log('  reminder month invalid : ' + badMonth.length);
console.log('  sample bad months      : ' + badMonth.slice(0, 4).map(r => r[5]).join(' / '));
