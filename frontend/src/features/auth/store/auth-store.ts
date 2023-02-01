import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import { shallow } from 'zustand/shallow';

import { SignInRequestDto, SignUpRequestDto, User } from '../common/types';
import { authApi } from '../services/auth-api.service';

type AuthState = {
  user?: User,
  token?: string
};

type AuthActions = {
  signIn: (request: SignInRequestDto) => void,
  signUp: (request: SignUpRequestDto) => void,
  signOut: () => void,
  updateCurrentUser: () => void
};

const useAuthStore = create<AuthState>()(
  persist((_) => ({
    user: undefined,
    token: undefined,
  }), {
    name: 'auth',
    partialize: (state) => state.token,
  }),
);

const authActions: AuthActions = {
  signIn: async (request) => {
    const response = await authApi.signIn(request);
    useAuthStore.setState({ ...response });
  },
  signOut: () => useAuthStore.setState({ token: undefined, user: undefined }),
  signUp: async (request) => {
    const response = await authApi.signUp(request);
    useAuthStore.setState({ ...response });
  },
  updateCurrentUser: async () => {
    const user = await authApi.getCurrentUser();
    useAuthStore.setState({ user });
  },
};
const useCurrentUser = () => useAuthStore((store) => store.user, shallow);

const getAuthToken = () => useAuthStore.getState().token;

export { authActions, getAuthToken, useCurrentUser };