import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';

import { action, SignUp } from '../components/SignUp';

const signUpRoute: RouteObject = {
  action,
  element: <SignUp />,
  path: AppRoute.SIGN_UP,
};

export { signUpRoute };
