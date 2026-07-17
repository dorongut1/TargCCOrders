// DashboardApi.ts
// Dashboard summary endpoint (GET /api/dashboard/summary)
import { api } from './client';

export interface DashboardMonthPoint {
  /** 'yyyy-MM' */
  month: string;
  revenue: number;
  orders: number;
}

export interface DashboardStatusCount {
  status: string;
  count: number;
}

export interface DashboardSummary {
  monthRevenue: number;
  yearRevenue: number;
  monthOrders: number;
  openOrders: number;
  unpaidOrders: number;
  openDebts: number;
  openDebtTotal: number;
  debtsNeedingAttention: number;
  pendingDeliveries: number;
  monthlySeries: DashboardMonthPoint[];
  ordersByStatus: DashboardStatusCount[];
}

export const DashboardApi = {
  getSummary: () =>
    api.get<DashboardSummary>('/dashboard/summary').then((r) => r.data),
};
