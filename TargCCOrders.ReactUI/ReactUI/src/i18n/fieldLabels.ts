// fieldLabels.ts — API field name → Hebrew label.
// Everything here resolves out of he.ts, so the dictionary stays the one
// place the vocabulary is decided.
import he from './he';

/**
 * Foreign keys and enum columns do not map onto `fields` one-for-one: an enum
 * column is labelled by what it means (enmPaymentMethod → אמצעי תשלום) and a
 * foreign key by the thing it points at (fkCustomerId → שם לקוח), because the
 * grid shows the referenced record's name rather than its id.
 */
const extra: Record<string, string> = {
  fkCustomerId: he.fields.customerName,
  fkProductId: he.fields.productName,
  fkOrderHeaderId: he.fields.orderNumber,
  productId: he.fields.productName,
  enmPaymentMethod: he.enums.paymentMethod,
  enmPaymentStatus: he.enums.paymentStatus,
  enmDeliveryMethod: he.enums.deliveryMethod,
  enmDeliveryDay: he.enums.deliveryDay,
  enmOrderStatus: he.enums.orderStatus,
  enmDeliveryStatus: he.enums.deliveryStatus,
  enmDebtStatus: he.enums.debtStatus,
  enmCustomerType: he.enums.customerType,
  enmCategory: he.enums.category,
  enmAccountantMethod: he.enums.accountantMethod,
  enmEmailStatus: he.enums.emailStatus,
};

const labels: Record<string, string> = {
  ...(he.fields as Record<string, string>),
  ...extra,
};

/** Hebrew label for an API field name, or undefined if we have none. */
export function fieldLabel(field: string): string | undefined {
  return labels[field];
}
