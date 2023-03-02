import { create } from 'zustand';
import { persist } from 'zustand/middleware';
import { shallow } from 'zustand/shallow';

import { handleHttpCalls } from '@/store/middlewares';

import { SignInRequestDto, SignUpRequestDto, User } from '../common/types';
import { authApi } from '../services/auth-api.service';

type AuthState = {
  user?: User;
  token?: string;
};

const initialState: AuthState = {
  user: undefined,
  token: undefined,
};

type AuthActions = {
  signIn: (request: SignInRequestDto) => void;
  signUp: (request: SignUpRequestDto) => void;
  signOut: () => void;
  loadCurrentUser: () => void;
};

const useAuthStore = create<AuthState>()(
  persist((_) => ({ ...initialState }), {
    name: 'auth',
    partialize: (state) => ({ token: state.token }),
  }),
);

const authActions = handleHttpCalls<AuthActions>({
  signIn: async (request) => {
    const response = await authApi.signIn(request);
    useAuthStore.setState({ ...response });
  },
  signOut: () => useAuthStore.setState({ ...initialState }),
  signUp: async (request) => {
    const response = await authApi.signUp(request);
    useAuthStore.setState({ ...response });
  },
  loadCurrentUser: async () => {
    const user = await authApi.getCurrentUser();
    useAuthStore.setState({ user });
  },
});

const useCurrentUser = () => useAuthStore((store) => store.user, shallow);

const getAuthToken = () => useAuthStore.getState().token;
const getCurrentUser = () => useAuthStore.getState().user;

export { authActions, getAuthToken, getCurrentUser, useCurrentUser };
