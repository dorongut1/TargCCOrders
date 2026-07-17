const express = require('express');
const app = express();
const PORT = 7185;

// Middleware
app.use(express.json());
app.use((req, res, next) => {
  const origin = req.headers.origin;
  if (origin === 'http://localhost:5173' || origin === 'http://localhost:5174') {
    res.header('Access-Control-Allow-Origin', origin);
  }
  res.header('Access-Control-Allow-Headers', 'Content-Type, Authorization');
  res.header('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  if (req.method === 'OPTIONS') return res.sendStatus(200);
  next();
});

// Helpers
const etag = () => Math.random().toString(16).slice(2, 18);
const d = (daysAgo) => {
  const dt = new Date();
  dt.setDate(dt.getDate() - daysAgo);
  return dt.toISOString().split('T')[0];
};
let nextIds = {};
function nextId(entity) {
  if (!nextIds[entity]) nextIds[entity] = 0;
  return ++nextIds[entity];
}

// ==================== AUTH ====================
app.post('/api/auth/login', (req, res) => {
  const { username, password } = req.body || {};
  if (username === 'DoronG' && password === 'Test') {
    return res.json({
      token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.' + Buffer.from(JSON.stringify({ sub: 'DoronG', iat: Date.now() })).toString('base64') + '.mock-signature',
      username: 'DoronG',
      displayName: 'דורון ג׳',
      expiresIn: 3600
    });
  }
  res.status(401).json({ message: 'שם משתמש או סיסמה שגויים' });
});

app.post('/api/auth/refresh', (req, res) => {
  res.json({
    token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.' + Buffer.from(JSON.stringify({ sub: 'DoronG', iat: Date.now() })).toString('base64') + '.mock-refreshed',
    expiresIn: 3600
  });
});

// ==================== ENUMS ====================
const enums = {
  enmCustomerType: ['רגיל', 'VIP', 'סיטונאי', 'קמעונאי'],
  enmDeliveryStatus: ['ממתין', 'בדרך', 'במרכז_לוגיסטי', 'נמסר'],
  enmDeliveryMethod: ['שליח', 'איסוף_עצמי', 'דואר'],
  enmPaymentMethod: ['מזומן', 'אשראי', 'העברה', 'שיק', 'ביט'],
  enmPaymentStatus: ['ממתין', 'שולם_חלקית', 'שולם'],
  enmOrderStatus: ['חדשה', 'בטיפול', 'הושלמה', 'בוטלה'],
  enmCategory: ['דבש', 'נרות', 'קוסמטיקה', 'מזון', 'אחר'],
  enmEmailStatus: ['טיוטה', 'נשלח', 'נכשל'],
  enmDeliveryDay: ['ראשון', 'שני', 'שלישי', 'רביעי', 'חמישי', 'שישי'],
  enmAccountantMethod: ['אימייל', 'פקס', 'ידני'],
  enmDebtStatus: ['פתוח', 'שולם_חלקית', 'שולם', 'באיחור']
};

app.get('/api/enums', (req, res) => res.json(enums));

// ==================== MOCK DATA ====================

// --- CUSTOMERS (15) ---
const customers = [
  { id: 1, customerCode: 'CUS-001', customerName: 'דבש הגליל', phone: '04-9871234', email: 'info@galil-honey.co.il', address: 'רחוב הדבורים 12', city: 'כרמיאל', taxId: '512345678', enmCustomerType: 1, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 2, customerCode: 'CUS-002', customerName: 'מתוק בטבע', phone: '03-5551234', email: 'orders@matok-bateva.co.il', address: 'שדרות רוטשילד 45', city: 'תל אביב', taxId: '523456789', enmCustomerType: 0, paymentTermsDays: 45, isActive: true, _etag: etag() },
  { id: 3, customerCode: 'CUS-003', customerName: 'נרות שלום', phone: '02-6234567', email: 'nerot@shalom.co.il', address: 'רחוב יפו 78', city: 'ירושלים', taxId: '534567890', enmCustomerType: 2, paymentTermsDays: 60, isActive: true, _etag: etag() },
  { id: 4, customerCode: 'CUS-004', customerName: 'טבע ודבש', phone: '08-6451234', email: 'info@teva-dvash.co.il', address: 'רחוב הנגב 5', city: 'באר שבע', taxId: '545678901', enmCustomerType: 0, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 5, customerCode: 'CUS-005', customerName: 'חנות הבריאות הירוקה', phone: '04-8321456', email: 'green@health.co.il', address: 'רחוב הרצל 22', city: 'חיפה', taxId: '556789012', enmCustomerType: 3, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 6, customerCode: 'CUS-006', customerName: 'סופר אורגני', phone: '09-7654321', email: 'super@organi.co.il', address: 'רחוב סוקולוב 10', city: 'נתניה', taxId: '567890123', enmCustomerType: 2, paymentTermsDays: 45, isActive: true, _etag: etag() },
  { id: 7, customerCode: 'CUS-007', customerName: 'הכוורת של יוסי', phone: '04-6781234', email: 'yossi@beehive.co.il', address: 'רחוב הגלבוע 3', city: 'עפולה', taxId: '578901234', enmCustomerType: 1, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 8, customerCode: 'CUS-008', customerName: 'מרכז הנרות', phone: '03-9876543', email: 'candles@center.co.il', address: 'רחוב אלנבי 56', city: 'תל אביב', taxId: '589012345', enmCustomerType: 0, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 9, customerCode: 'CUS-009', customerName: 'פארם טבעי', phone: '08-9234567', email: 'farm@natural.co.il', address: 'רחוב הפרחים 8', city: 'אשדוד', taxId: '590123456', enmCustomerType: 3, paymentTermsDays: 60, isActive: true, _etag: etag() },
  { id: 10, customerCode: 'CUS-010', customerName: 'דבש השרון', phone: '09-8612345', email: 'sharon@honey.co.il', address: 'רחוב הדרים 15', city: 'רעננה', taxId: '501234567', enmCustomerType: 1, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 11, customerCode: 'CUS-011', customerName: 'קוסמטיקה מהטבע', phone: '03-7771234', email: 'cosmetics@nature.co.il', address: 'רחוב דיזנגוף 120', city: 'תל אביב', taxId: '512345670', enmCustomerType: 0, paymentTermsDays: 45, isActive: true, _etag: etag() },
  { id: 12, customerCode: 'CUS-012', customerName: 'שוק הדבש', phone: '02-5439876', email: 'market@dvash.co.il', address: 'רחוב מחנה יהודה 33', city: 'ירושלים', taxId: '523456780', enmCustomerType: 2, paymentTermsDays: 30, isActive: false, _etag: etag() },
  { id: 13, customerCode: 'CUS-013', customerName: 'מכולת השכונה', phone: '04-8123456', email: 'makolet@shchuna.co.il', address: 'רחוב מוריה 7', city: 'חיפה', taxId: '534567801', enmCustomerType: 3, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 14, customerCode: 'CUS-014', customerName: 'ביו שופ', phone: '08-6789012', email: 'bio@shop.co.il', address: 'רחוב קרן היסוד 19', city: 'אשקלון', taxId: '545678012', enmCustomerType: 0, paymentTermsDays: 30, isActive: true, _etag: etag() },
  { id: 15, customerCode: 'CUS-015', customerName: 'דליקטסן הגולן', phone: '04-6823456', email: 'golan@deli.co.il', address: 'רחוב התעשייה 2', city: 'קצרין', taxId: '556780123', enmCustomerType: 1, paymentTermsDays: 45, isActive: true, _etag: etag() }
];
nextIds.customers = 15;

// --- CUSTOMER DEBTS (20) ---
const customerDebts = [
  { id: 1, fkCustomerId: 1, debtAmount: 2500, paidAmount: 1000, remainingAmount: 1500, debtDate: d(60), dueDate: d(-10), enmDebtStatus: 1, _etag: etag() },
  { id: 2, fkCustomerId: 2, debtAmount: 1200, paidAmount: 1200, remainingAmount: 0, debtDate: d(45), dueDate: d(5), enmDebtStatus: 2, _etag: etag() },
  { id: 3, fkCustomerId: 3, debtAmount: 4800, paidAmount: 0, remainingAmount: 4800, debtDate: d(30), dueDate: d(-5), enmDebtStatus: 3, _etag: etag() },
  { id: 4, fkCustomerId: 4, debtAmount: 350, paidAmount: 350, remainingAmount: 0, debtDate: d(90), dueDate: d(30), enmDebtStatus: 2, _etag: etag() },
  { id: 5, fkCustomerId: 5, debtAmount: 1800, paidAmount: 500, remainingAmount: 1300, debtDate: d(20), dueDate: d(-15), enmDebtStatus: 1, _etag: etag() },
  { id: 6, fkCustomerId: 6, debtAmount: 3200, paidAmount: 0, remainingAmount: 3200, debtDate: d(10), dueDate: d(-30), enmDebtStatus: 0, _etag: etag() },
  { id: 7, fkCustomerId: 7, debtAmount: 900, paidAmount: 900, remainingAmount: 0, debtDate: d(120), dueDate: d(60), enmDebtStatus: 2, _etag: etag() },
  { id: 8, fkCustomerId: 8, debtAmount: 2100, paidAmount: 700, remainingAmount: 1400, debtDate: d(25), dueDate: d(-8), enmDebtStatus: 1, _etag: etag() },
  { id: 9, fkCustomerId: 9, debtAmount: 4500, paidAmount: 0, remainingAmount: 4500, debtDate: d(15), dueDate: d(-20), enmDebtStatus: 3, _etag: etag() },
  { id: 10, fkCustomerId: 10, debtAmount: 670, paidAmount: 670, remainingAmount: 0, debtDate: d(80), dueDate: d(20), enmDebtStatus: 2, _etag: etag() },
  { id: 11, fkCustomerId: 11, debtAmount: 1500, paidAmount: 0, remainingAmount: 1500, debtDate: d(5), dueDate: d(-25), enmDebtStatus: 0, _etag: etag() },
  { id: 12, fkCustomerId: 12, debtAmount: 3800, paidAmount: 2000, remainingAmount: 1800, debtDate: d(40), dueDate: d(-2), enmDebtStatus: 1, _etag: etag() },
  { id: 13, fkCustomerId: 13, debtAmount: 250, paidAmount: 250, remainingAmount: 0, debtDate: d(100), dueDate: d(50), enmDebtStatus: 2, _etag: etag() },
  { id: 14, fkCustomerId: 14, debtAmount: 1100, paidAmount: 0, remainingAmount: 1100, debtDate: d(8), dueDate: d(-12), enmDebtStatus: 0, _etag: etag() },
  { id: 15, fkCustomerId: 15, debtAmount: 5000, paidAmount: 3000, remainingAmount: 2000, debtDate: d(50), dueDate: d(-3), enmDebtStatus: 1, _etag: etag() },
  { id: 16, fkCustomerId: 1, debtAmount: 800, paidAmount: 0, remainingAmount: 800, debtDate: d(3), dueDate: d(-27), enmDebtStatus: 0, _etag: etag() },
  { id: 17, fkCustomerId: 3, debtAmount: 2200, paidAmount: 2200, remainingAmount: 0, debtDate: d(70), dueDate: d(10), enmDebtStatus: 2, _etag: etag() },
  { id: 18, fkCustomerId: 5, debtAmount: 1600, paidAmount: 400, remainingAmount: 1200, debtDate: d(18), dueDate: d(-7), enmDebtStatus: 3, _etag: etag() },
  { id: 19, fkCustomerId: 7, debtAmount: 3100, paidAmount: 0, remainingAmount: 3100, debtDate: d(12), dueDate: d(-18), enmDebtStatus: 0, _etag: etag() },
  { id: 20, fkCustomerId: 10, debtAmount: 450, paidAmount: 450, remainingAmount: 0, debtDate: d(55), dueDate: d(15), enmDebtStatus: 2, _etag: etag() }
];
nextIds.customerDebts = 20;

// --- ORDER HEADERS (20) ---
const orderHeaders = [];
for (let i = 1; i <= 20; i++) {
  const totalAmount = Math.round((Math.random() * 8000 + 500) * 100) / 100;
  const vatAmount = Math.round(totalAmount * 0.17 * 100) / 100;
  orderHeaders.push({
    id: i,
    orderNumber: `ORD-${String(i).padStart(3, '0')}`,
    fkCustomerId: ((i - 1) % 15) + 1,
    orderDate: d(Math.floor(Math.random() * 90)),
    totalAmount,
    vatAmount,
    totalWithVat: Math.round((totalAmount + vatAmount) * 100) / 100,
    enmPaymentStatus: i % 3,
    enmOrderStatus: i % 4,
    enmPaymentMethod: i % 5,
    enmDeliveryMethod: i % 3,
    notes: '',
    _etag: etag()
  });
}
nextIds.orderHeaders = 20;

// --- DELIVERIES (20) ---
const deliveryAddresses = [
  'רחוב הרצל 10, תל אביב', 'שדרות בן גוריון 25, חיפה', 'רחוב יפו 44, ירושלים',
  'רחוב ויצמן 8, כפר סבא', 'רחוב סוקולוב 33, רמת גן', 'רחוב ז׳בוטינסקי 15, פתח תקווה',
  'רחוב הנשיא 7, ראשון לציון', 'רחוב רוטשילד 50, נתניה', 'רחוב הגפן 12, רעננה',
  'שדרות ירושלים 20, אשדוד', 'רחוב בלפור 3, באר שבע', 'רחוב העצמאות 18, חולון',
  'רחוב הרב קוק 6, בני ברק', 'רחוב אהרונוביץ 9, הרצליה', 'רחוב המלך דוד 14, עפולה',
  'רחוב הפלמ״ח 21, קריית שמונה', 'רחוב הגליל 4, טבריה', 'שדרות הנשיא 30, עכו',
  'רחוב התעשייה 11, אילת', 'רחוב הדקל 2, קצרין'
];

const deliveries = [];
for (let i = 1; i <= 20; i++) {
  deliveries.push({
    id: i,
    fkOrderHeaderId: i,
    deliveryAddress: deliveryAddresses[i - 1],
    contactPhone: `05${Math.floor(Math.random() * 10)}-${String(Math.floor(Math.random() * 9000000 + 1000000))}`,
    enmDeliveryStatus: i % 4,
    scheduledDate: d(-Math.floor(Math.random() * 14)),
    actualDeliveryDate: i % 4 === 3 ? d(-Math.floor(Math.random() * 7)) : null,
    driverNotes: i % 3 === 0 ? 'להתקשר לפני הגעה' : '',
    _etag: etag()
  });
}
nextIds.deliveries = 20;

// --- PRODUCTS (10) ---
const products = [
  { id: 1, productCode: 'PRD-001', productName: 'דבש טהור 500 גרם', enmCategory: 0, unitOfMeasure: 'יחידה', isActive: true, baseCost: 35, _etag: etag() },
  { id: 2, productCode: 'PRD-002', productName: 'נרות שעווה טבעית', enmCategory: 1, unitOfMeasure: 'חבילה', isActive: true, baseCost: 28, _etag: etag() },
  { id: 3, productCode: 'PRD-003', productName: 'קרם דבש לפנים', enmCategory: 2, unitOfMeasure: 'יחידה', isActive: true, baseCost: 45, _etag: etag() },
  { id: 4, productCode: 'PRD-004', productName: 'דבש פרחי בר 1 ק״ג', enmCategory: 0, unitOfMeasure: 'יחידה', isActive: true, baseCost: 62, _etag: etag() },
  { id: 5, productCode: 'PRD-005', productName: 'נרות דקורטיביים צבעוניים', enmCategory: 1, unitOfMeasure: 'סט', isActive: true, baseCost: 55, _etag: etag() },
  { id: 6, productCode: 'PRD-006', productName: 'סבון דבש טבעי', enmCategory: 2, unitOfMeasure: 'יחידה', isActive: true, baseCost: 22, _etag: etag() },
  { id: 7, productCode: 'PRD-007', productName: 'מארז דגימות דבש', enmCategory: 3, unitOfMeasure: 'מארז', isActive: true, baseCost: 85, _etag: etag() },
  { id: 8, productCode: 'PRD-008', productName: 'שעוות דבורים גולמית', enmCategory: 4, unitOfMeasure: 'ק״ג', isActive: true, baseCost: 120, _etag: etag() },
  { id: 9, productCode: 'PRD-009', productName: 'דבש אקליפטוס 350 גרם', enmCategory: 0, unitOfMeasure: 'יחידה', isActive: true, baseCost: 42, _etag: etag() },
  { id: 10, productCode: 'PRD-010', productName: 'ערכת נרות שבת', enmCategory: 1, unitOfMeasure: 'חבילה', isActive: false, baseCost: 38, _etag: etag() }
];
nextIds.products = 10;

// --- ORDER LINES (40) ---
const orderLines = [];
for (let i = 1; i <= 40; i++) {
  const fkOrderHeaderId = ((i - 1) % 20) + 1;
  const fkProductId = ((i - 1) % 10) + 1;
  const quantity = Math.floor(Math.random() * 20) + 1;
  const unitPrice = products[fkProductId - 1].baseCost * (1 + Math.random() * 0.5);
  const discountPercent = [0, 0, 5, 10, 15][i % 5];
  const lineTotal = Math.round(quantity * unitPrice * (1 - discountPercent / 100) * 100) / 100;
  orderLines.push({
    id: i,
    fkOrderHeaderId,
    fkProductId,
    quantity,
    unitPrice: Math.round(unitPrice * 100) / 100,
    discountPercent,
    lineTotal,
    _etag: etag()
  });
}
nextIds.orderLines = 40;

// --- PRODUCT PRICES (20) ---
const productPrices = [];
for (let i = 1; i <= 20; i++) {
  const fkProductId = ((i - 1) % 10) + 1;
  const base = products[fkProductId - 1].baseCost;
  productPrices.push({
    id: i,
    fkProductId,
    enmCustomerType: ((i - 1) % 4),
    sellingPrice: Math.round(base * (1.3 + Math.random() * 0.4) * 100) / 100,
    minQuantity: [1, 5, 10, 20, 50][i % 5],
    discountPercent: [0, 3, 5, 8, 10][i % 5],
    _etag: etag()
  });
}
nextIds.productPrices = 20;

// --- PRODUCT PRICE HISTORY (15) ---
const productPriceHists = [];
for (let i = 1; i <= 15; i++) {
  const fkProductId = ((i - 1) % 10) + 1;
  const base = products[fkProductId - 1].baseCost;
  productPriceHists.push({
    id: i,
    fkProductId,
    enmCustomerType: ((i - 1) % 4),
    oldPrice: Math.round(base * (1.1 + Math.random() * 0.3) * 100) / 100,
    newPrice: Math.round(base * (1.3 + Math.random() * 0.4) * 100) / 100,
    changeDate: d(Math.floor(Math.random() * 180) + 30),
    changedBy: 'DoronG',
    _etag: etag()
  });
}
nextIds.productPriceHists = 15;

// --- SUPPLIER ORDERS (10) ---
const supplierOrders = [
  { id: 1, fkOrderHeaderId: 1, supplierEmail: 'supplier1@honey.co.il', emailSubject: 'הזמנה חדשה - דבש טהור', emailBody: 'שלום רב, מצורפת הזמנה חדשה.', enmEmailStatus: 1, sentDate: d(5), _etag: etag() },
  { id: 2, fkOrderHeaderId: 3, supplierEmail: 'orders@candles-il.co.il', emailSubject: 'הזמנת נרות - דחוף', emailBody: 'נא לאשר הזמנה בהקדם.', enmEmailStatus: 1, sentDate: d(10), _etag: etag() },
  { id: 3, fkOrderHeaderId: 5, supplierEmail: 'supplier1@honey.co.il', emailSubject: 'הזמנה חוזרת - דבש פרחי בר', emailBody: 'הזמנה חוזרת ללקוח קבוע.', enmEmailStatus: 0, sentDate: null, _etag: etag() },
  { id: 4, fkOrderHeaderId: 7, supplierEmail: 'cosmetics@natural.co.il', emailSubject: 'הזמנת קוסמטיקה טבעית', emailBody: 'מצורפת רשימת מוצרים להזמנה.', enmEmailStatus: 1, sentDate: d(15), _etag: etag() },
  { id: 5, fkOrderHeaderId: 9, supplierEmail: 'supplier2@bees.co.il', emailSubject: 'הזמנת שעווה גולמית', emailBody: 'נדרשים 50 ק״ג שעווה.', enmEmailStatus: 2, sentDate: d(8), _etag: etag() },
  { id: 6, fkOrderHeaderId: 11, supplierEmail: 'supplier1@honey.co.il', emailSubject: 'הזמנה מרוכזת - חודש ינואר', emailBody: 'הזמנה מרוכזת לחודש הקרוב.', enmEmailStatus: 1, sentDate: d(20), _etag: etag() },
  { id: 7, fkOrderHeaderId: 13, supplierEmail: 'orders@candles-il.co.il', emailSubject: 'הזמנת נרות שבת - מיוחד', emailBody: 'הזמנה מיוחדת לחגים.', enmEmailStatus: 1, sentDate: d(3), _etag: etag() },
  { id: 8, fkOrderHeaderId: 15, supplierEmail: 'cosmetics@natural.co.il', emailSubject: 'הזמנת סבונים ומוצרי טיפוח', emailBody: 'נא לשלוח הצעת מחיר מעודכנת.', enmEmailStatus: 0, sentDate: null, _etag: etag() },
  { id: 9, fkOrderHeaderId: 17, supplierEmail: 'supplier2@bees.co.il', emailSubject: 'בקשה לדגימות דבש חדשות', emailBody: 'נבקש דגימות מזני דבש חדשים.', enmEmailStatus: 1, sentDate: d(12), _etag: etag() },
  { id: 10, fkOrderHeaderId: 19, supplierEmail: 'supplier1@honey.co.il', emailSubject: 'הזמנה דחופה - מלאי נמוך', emailBody: 'המלאי נמוך, נדרשת אספקה מהירה.', enmEmailStatus: 2, sentDate: d(1), _etag: etag() }
];
nextIds.supplierOrders = 10;

// --- BEEHIVE BUYER TRACKINGS (8) ---
const beehiveBuyerTrackings = [
  { id: 1, fkCustomerId: 1, lastOrderDate: d(10), beehiveQuantity: 5, reminderMonth: 3, isRelevant: true, notes: 'לקוח קבוע - דבש הגליל', _etag: etag() },
  { id: 2, fkCustomerId: 2, lastOrderDate: d(45), beehiveQuantity: 2, reminderMonth: 6, isRelevant: true, notes: 'מזמין לחנות בתל אביב', _etag: etag() },
  { id: 3, fkCustomerId: 4, lastOrderDate: d(90), beehiveQuantity: 8, reminderMonth: 1, isRelevant: true, notes: 'לקוח גדול מהנגב', _etag: etag() },
  { id: 4, fkCustomerId: 7, lastOrderDate: d(30), beehiveQuantity: 12, reminderMonth: 4, isRelevant: true, notes: 'כוורן עצמאי - שיתוף פעולה', _etag: etag() },
  { id: 5, fkCustomerId: 10, lastOrderDate: d(120), beehiveQuantity: 3, reminderMonth: 9, isRelevant: false, notes: 'הפסיק להזמין זמנית', _etag: etag() },
  { id: 6, fkCustomerId: 12, lastOrderDate: d(200), beehiveQuantity: 1, reminderMonth: 12, isRelevant: false, notes: 'לקוח לא פעיל', _etag: etag() },
  { id: 7, fkCustomerId: 14, lastOrderDate: d(15), beehiveQuantity: 6, reminderMonth: 2, isRelevant: true, notes: 'מעוניין בהרחבה', _etag: etag() },
  { id: 8, fkCustomerId: 15, lastOrderDate: d(60), beehiveQuantity: 4, reminderMonth: 7, isRelevant: true, notes: 'לקוח מהגולן - איכותי', _etag: etag() }
];
nextIds.beehiveBuyerTrackings = 8;

// ==================== GENERIC CRUD ====================

const collections = {
  customers,
  customerDebts,
  deliveries,
  orderHeaders,
  orderLines,
  products,
  productPrices,
  productPriceHists,
  supplierOrders,
  beehiveBuyerTrackings
};

// Search field mappings per entity
const searchFields = {
  customers: ['customerName', 'customerCode', 'city', 'email'],
  customerDebts: [],
  deliveries: ['deliveryAddress', 'contactPhone'],
  orderHeaders: ['orderNumber'],
  orderLines: [],
  products: ['productName', 'productCode'],
  productPrices: [],
  productPriceHists: [],
  supplierOrders: ['emailSubject', 'supplierEmail'],
  beehiveBuyerTrackings: ['notes']
};

function registerCrud(entityName) {
  const path = `/api/${entityName}`;
  const col = collections[entityName];
  const fields = searchFields[entityName] || [];

  // GET list with pagination
  app.get(path, (req, res) => {
    const page = parseInt(req.query.page) || 0;
    const pageSize = parseInt(req.query.pageSize) || 25;
    const search = (req.query.search || '').toLowerCase();
    const sortField = req.query.sortField;
    const sortDir = req.query.sortDir === 'desc' ? -1 : 1;

    let filtered = col;
    if (search && fields.length > 0) {
      filtered = col.filter(item =>
        fields.some(f => item[f] && String(item[f]).toLowerCase().includes(search))
      );
    }

    if (sortField) {
      filtered = [...filtered].sort((a, b) => {
        const av = a[sortField], bv = b[sortField];
        if (av == null) return 1;
        if (bv == null) return -1;
        if (typeof av === 'string') return av.localeCompare(bv) * sortDir;
        return (av - bv) * sortDir;
      });
    }

    const total = filtered.length;
    const items = filtered.slice(page * pageSize, (page + 1) * pageSize);
    res.json({ items, total });
  });

  // GET by id
  app.get(`${path}/:id`, (req, res) => {
    const item = col.find(x => x.id === parseInt(req.params.id));
    if (!item) return res.status(404).json({ message: 'לא נמצא' });
    res.json(item);
  });

  // POST
  app.post(path, (req, res) => {
    const item = { ...req.body, id: nextId(entityName), _etag: etag() };
    col.push(item);
    res.status(201).json(item);
  });

  // PUT
  app.put(`${path}/:id`, (req, res) => {
    const idx = col.findIndex(x => x.id === parseInt(req.params.id));
    if (idx === -1) return res.status(404).json({ message: 'לא נמצא' });
    col[idx] = { ...col[idx], ...req.body, id: col[idx].id, _etag: etag() };
    res.json(col[idx]);
  });

  // DELETE
  app.delete(`${path}/:id`, (req, res) => {
    const idx = col.findIndex(x => x.id === parseInt(req.params.id));
    if (idx === -1) return res.status(404).json({ message: 'לא נמצא' });
    col.splice(idx, 1);
    res.status(204).send();
  });
}

// Register all entities
Object.keys(collections).forEach(registerCrud);

// ==================== START SERVER ====================
app.listen(PORT, () => {
  console.log(`Mock API server running at http://localhost:${PORT}`);
  console.log('Available entities:', Object.keys(collections).join(', '));
});
