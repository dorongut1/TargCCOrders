// ParametersApi.ts
// Management of how the system's lists are presented.
//
// The values themselves come from the compiled enum and cannot be added here
// — see SPIKE_1.1_ENUM_EXTENSIBILITY_2026-08-18.md. What this manages is the
// label, the order, whether a value is still offered, and the delivery flag.
import { api } from './client';

export interface ParameterValue {
  enumType: string;
  enumValue: string;
  label: string;
  isActive: boolean;
  isDelivery: boolean;
  sortOrder: number;
  /** Live records using this value — shown so nobody hides one in use. */
  usageCount: number;
}

export interface ParameterType {
  enumType: string;
  label: string;
  /** Only the delivery-method list has a meaningful IsDelivery flag. */
  showDeliveryFlag: boolean;
  values: ParameterValue[];
}

export interface UpdateParameterPayload {
  label: string;
  isActive: boolean;
  isDelivery: boolean;
  sortOrder: number;
}

export const ParametersApi = {
  list: async (): Promise<ParameterType[]> => {
    const { data } = await api.get<ParameterType[]>('/parameters');
    return data ?? [];
  },

  update: async (enumType: string, enumValue: string, payload: UpdateParameterPayload) => {
    const { data } = await api.put(
      `/parameters/${encodeURIComponent(enumType)}/${encodeURIComponent(enumValue)}`,
      payload,
    );
    return data as { message: string };
  },
};
