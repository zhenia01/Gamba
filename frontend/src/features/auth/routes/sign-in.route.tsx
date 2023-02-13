import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';

import { action, SignIn } from '../components/SignIn';

const signInRoute: RouteObject = {
  action,
  element: <SignIn />,
  path: AppRoute.SIGN_IN,
};

export { signInRoute };
