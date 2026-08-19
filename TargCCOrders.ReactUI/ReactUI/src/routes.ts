// routes.ts - Route definitions with sidebar grouping
// TargCCOrders — Hebrew-first navigation structure

export interface RouteDefinition {
  path: string;
  label: string;
  entity: string;
  group: string;
  readOnly: boolean;
  /** Composite screens (not auto-generated CRUD) */
  isComposite?: boolean;
  /** Hidden from the sidebar navigation (route stays registered for deep links) */
  hideInNav?: boolean;
  /**
   * Hand-written page that registers its own route in App.tsx and has no
   * generated List/Form/View trio. Listed here only so it reaches the sidebar;
   * the automatic route generation skips it.
   */
  standalone?: boolean;
}

export const sidebarGroups = [
  'הזמנות',
  'לקוחות',
  'מוצרים',
  'משלוחים',
  'כספים',
  'כוורות',
  'ניהול',
] as const;

export const entityRoutes: RouteDefinition[] = [
  // ── הזמנות (Orders) ──
  { path: '/orders', label: 'הזמנות', entity: 'orderComposite', group: 'הזמנות', readOnly: false, isComposite: true },
  // Raw generated CRUD screens — hidden from the sidebar (the composite orders screen replaces them), routes kept for deep links
  { path: '/orderHeaders', label: 'כותרות הזמנה', entity: 'orderHeader', group: 'הזמנות', readOnly: false, hideInNav: true },
  { path: '/orderLines', label: 'שורות הזמנה', entity: 'orderLine', group: 'הזמנות', readOnly: false, hideInNav: true },
  { path: '/supplierOrders', label: 'הזמנות ספקים', entity: 'supplierOrder', group: 'הזמנות', readOnly: false },

  // ── לקוחות (Customers) ──
  { path: '/customers', label: 'לקוחות', entity: 'customer', group: 'לקוחות', readOnly: false },

  // ── מוצרים (Products) ──
  { path: '/products', label: 'מוצרים', entity: 'product', group: 'מוצרים', readOnly: false },
  { path: '/productPrices', label: 'מחירי מוצרים', entity: 'productPrice', group: 'מוצרים', readOnly: false },
  { path: '/productPriceHists', label: 'היסטוריית מחירים', entity: 'productPriceHist', group: 'מוצרים', readOnly: true },

  // ── משלוחים (Deliveries) ──
  { path: '/delivery-board', label: 'לוח משלוחים', entity: 'deliveryWorkflow', group: 'משלוחים', readOnly: false, isComposite: true },
  { path: '/deliveries', label: 'רשימת משלוחים', entity: 'delivery', group: 'משלוחים', readOnly: false },

  // ── כספים (Finance) ──
  { path: '/debt-management', label: 'ניהול חובות', entity: 'debtManagement', group: 'כספים', readOnly: false, isComposite: true },
  { path: '/customerDebts', label: 'חובות לקוחות', entity: 'customerDebt', group: 'כספים', readOnly: false, hideInNav: true },

  // ── כוורות (Beehive) ──
  { path: '/beehiveBuyerTrackings', label: 'מעקב כוורות', entity: 'beehiveBuyerTracking', group: 'כוורות', readOnly: false },

  // ── ניהול (Administration) ──
  // The API behind this screen is gated on the AdminUI policy, so a
  // non-administrator who follows the link gets an empty screen rather than
  // data. Listed because it was previously reachable only by typing the URL.
  { path: '/users', label: 'ניהול משתמשים', entity: 'userAdmin', group: 'ניהול', readOnly: false, standalone: true },
  { path: '/parameters', label: 'ניהול פרמטרים', entity: 'parameters', group: 'ניהול', readOnly: false, standalone: true },
];

export const getListPath = (entity: string) => `/${entity}`;
export const getNewPath = (entity: string) => `/${entity}/new`;
export const getViewPath = (entity: string, id: string | number) => `/${entity}/${id}`;
export const getEditPath = (entity: string, id: string | number) => `/${entity}/${id}/edit`;

