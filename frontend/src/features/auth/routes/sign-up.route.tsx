import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';

import { action } from '../components/sign-up/action';
import { SignUp } from '../components/sign-up/SignUp';

const signUpRoute: RouteObject = {
  action,
  element: <SignUp />,
  path: AppRoute.SIGN_UP,
};

export { signUpRoute };
