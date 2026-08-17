// QA report over the exported CSVs. Read-only, no DB access — node handles the
// Hebrew correctly whereas sqlcmd's console mangles it to '?'.
const fs = require('fs');
const DIR = 'C:\\Dev\\NonTFS\\TargCCOrders\\Database\\import\\';

function readCsv(file) {
  const txt = fs.readFileSync(DIR + file, 'utf8').replace(/^\uFEFF/, '');
  const rows = [];
  let cur = [], val = '', inQ = false;
  for (let i = 0; i < txt.length; i++) {
    const ch = txt[i];
    if (inQ) {
      if (ch === '"') { if (txt[i + 1] === '"') { val += '"'; i++; } else inQ = false; }
      else val += ch;
    } else if (ch === '"') inQ = true;
    else if (ch === ',') { cur.push(val); val = ''; }
    else if (ch === '\r') { /* skip */ }
    else if (ch === '\n') { cur.push(val); rows.push(cur); cur = []; val = ''; }
    else val += ch;
  }
  if (val !== '' || cur.length) { cur.push(val); rows.push(cur); }
  return rows.slice(1).filter(r => r.some(c => c.trim() !== ''));
}

function tally(rows, idx) {
  const m = new Map();
  rows.forEach(r => {
    const v = (r[idx] || '').trim() || '(empty)';
    m.set(v, (m.get(v) || 0) + 1);
  });
  return [...m.entries()].sort((a, b) => b[1] - a[1]);
}

module.exports = { readCsv, tally, DIR };
