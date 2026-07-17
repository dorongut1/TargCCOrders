// OrderCompositeApi.ts
// Atomic order save/load endpoints (/api/orders/composite)
import { api } from './client';
import type { OrderHeader, CreateOrderHeaderRequest } from '../types/OrderHeader';
import type { OrderLine } from '../types/OrderLine';

export interface OrderHeaderDto extends OrderHeader {
  customerDisplayName?: string | null;
}

export interface OrderLineDto extends OrderLine {
  productDisplayName?: string | null;
}

export interface OrderCompositeDto {
  header: OrderHeaderDto;
  lines: OrderLineDto[];
}

export interface CompositeLinePayload {
  /** Existing line id, or 0 for a new line */
  id: number;
  fkOrderHeaderId: number;
  fkProductId: number;
  quantity: number;
  unitPrice: number;
  discountPercent: number | null;
  lineNumber: number;
}

export interface CompositeSavePayload {
  header: CreateOrderHeaderRequest;
  lines: CompositeLinePayload[];
  /** IDs of existing lines removed by the user */
  deletedLineIds: number[];
}

export interface CompositeSaveResponse {
  header: OrderHeaderDto;
  savedLines?: OrderLineDto[];
  errors: string[];
}

export const OrderCompositeApi = {
  getComposite: (id: number | string) =>
    api.get<OrderCompositeDto>(`/orders/composite/${id}`).then((r) => r.data),

  createComposite: (payload: CompositeSavePayload) =>
    api.post<CompositeSaveResponse>('/orders/composite', payload).then((r) => r.data),

  updateComposite: (id: number | string, payload: CompositeSavePayload) =>
    api.put<CompositeSaveResponse>(`/orders/composite/${id}`, payload).then((r) => r.data),

  getNextOrderNumber: () =>
    api.get<{ nextOrderNumber: number }>('/orders/nextNumber').then((r) => r.data),
};
