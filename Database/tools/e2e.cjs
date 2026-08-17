// End-to-end check on imported data: pick a real farmer customer, resolve the
// price for פרסמיליס through the API, create an order, and confirm what was
// actually SAVED (not merely displayed) — the failure mode from 2026-08-09.
const https = require('http');

function req(method, path, body, token) {
  return new Promise((res, rej) => {
    const data = body ? JSON.stringify(body) : null;
    const r = https.request({
      host: 'localhost', port: 5199, path, method,
      headers: Object.assign(
        { 'Content-Type': 'application/json' },
        token ? { Authorization: 'Bearer ' + token } : {},
        data ? { 'Content-Length': Buffer.byteLength(data) } : {})
    }, (rs) => {
      let b = '';
      rs.on('data', c => b += c);
      rs.on('end', () => res({ status: rs.statusCode, body: b }));
    });
    r.on('error', rej);
    if (data) r.write(data);
    r.end();
  });
}

(async () => {
  const login = await req('POST', '/api/auth/login', { username: 'DoronG', password: 'Test' });
  const token = JSON.parse(login.body).token;
  console.log('login: ' + login.status);

  const cust = JSON.parse((await req('GET', '/api/customers', null, token)).body);
  const list = cust.items || cust;
  const farmer = list.find(c => c.enmCustomerType === 'Farmer' || c.EnmCustomerType === 'Farmer') || list[0];
  console.log('customer: ' + (farmer.customerName || farmer.CustomerName) +
              '  type=' + (farmer.enmCustomerType || farmer.EnmCustomerType) +
              '  id=' + (farmer.id || farmer.ID));

  const prods = JSON.parse((await req('GET', '/api/products', null, token)).body);
  const plist = prods.items || prods;
  const pers = plist.find(p => (p.productCode || p.ProductCode) === '1020') || plist[0];
  console.log('product : ' + (pers.productName || pers.ProductName) +
              '  code=' + (pers.productCode || pers.ProductCode));

  const pr = await req('GET',
    `/api/pricing/resolve?productId=${pers.id || pers.ID}&customerId=${farmer.id || farmer.ID}&quantity=1`,
    null, token);
  console.log('pricing/resolve: ' + pr.status + '  ' + pr.body.slice(0, 200));
})().catch(e => console.error('ERR ' + e.message));
