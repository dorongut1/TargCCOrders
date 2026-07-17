// PricingApi.ts
// Price resolution endpoint (GET /api/pricing/resolve)
import { api } from './client';

export interface PriceResolveResult {
  found: boolean;
  customerType: string;
  unitPrice?: number;
  discountPercent?: number;
  minQuantity?: number;
  priceId?: number;
}

export const PricingApi = {
  resolve: (productId: number, customerId: number, quantity: number) =>
    api.get<PriceResolveResult>('/pricing/resolve', {
      params: { productId, customerId, quantity },
    }).then((r) => r.data),
};
