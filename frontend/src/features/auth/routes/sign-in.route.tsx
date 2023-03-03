import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';

import { action } from '../components/sign-in/action';
import { SignIn } from '../components/sign-in/SignIn';

const signInRoute: RouteObject = {
  action,
  element: <SignIn />,
  path: AppRoute.SIGN_IN,
};

export { signInRoute };
