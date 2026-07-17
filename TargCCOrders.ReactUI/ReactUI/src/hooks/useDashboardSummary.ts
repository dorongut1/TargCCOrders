// useDashboardSummary.ts
import { useQuery } from '@tanstack/react-query';
import { DashboardApi } from '../api/DashboardApi';

export function useDashboardSummary() {
  return useQuery({
    queryKey: ['dashboard', 'summary'],
    queryFn: DashboardApi.getSummary,
    staleTime: 60_000,
    refetchInterval: 5 * 60_000,
  });
}
