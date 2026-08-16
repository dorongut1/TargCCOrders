// UserAdminApi.ts — client for the user administration endpoints.
// Backed by ccUserAdminController, which enforces the Master /
// ApplicationMaster / UserManager gate server-side. Hiding the menu is a
// convenience, not the security boundary.
import { api } from './client';

export interface AdminUser {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  roleId: number;
  roleName: string;
  isDisabled: boolean;
  isLockedOut: boolean;
  /** False means TargCC will refuse this user's login, whatever the password. */
  hasApplicationAccess: boolean;
  datePasswordChanged: string;
}

export interface AdminRole {
  id: number;
  name: string;
}

export interface MeInfo {
  userId: number;
  userName: string;
  fullName: string;
  roles: string[];
  canManageUsers: boolean;
}

export interface CreateUserPayload {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  roleId: number;
}

export const UserAdminApi = {
  me: async (): Promise<MeInfo> => {
    const { data } = await api.get<MeInfo>('/userAdmin/me');
    return data;
  },

  list: async (search = ''): Promise<AdminUser[]> => {
    const { data } = await api.get<{ items: AdminUser[] }>('/userAdmin/users', {
      params: search ? { search } : undefined,
    });
    return data.items ?? [];
  },

  roles: async (): Promise<AdminRole[]> => {
    const { data } = await api.get<AdminRole[]>('/userAdmin/roles');
    return data ?? [];
  },

  create: async (payload: CreateUserPayload) => {
    const { data } = await api.post('/userAdmin/users', payload);
    return data as {
      user: AdminUser;
      initialPassword: string | null;
      message: string;
      warnings: string[];
    };
  },

  resetPassword: async (id: number) => {
    const { data } = await api.post(`/userAdmin/users/${id}/resetPassword`);
    return data as { message: string; password: string; changed: boolean };
  },

  unlock: async (id: number) => {
    const { data } = await api.post(`/userAdmin/users/${id}/unlock`);
    return data as { message: string; user: AdminUser };
  },

  changeMyPassword: async (currentPassword: string, newPassword: string) => {
    const { data } = await api.post('/userAdmin/changeMyPassword', {
      currentPassword,
      newPassword,
    });
    return data as { message: string };
  },
};
