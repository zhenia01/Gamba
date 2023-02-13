import { NonIndexRouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';

import { Auth } from '../components/Auth';
import { signInRoute } from './sign-in.route';
import { signUpRoute } from './sign-up.route';

const authRoute: NonIndexRouteObject = {
  path: AppRoute.AUTH,
  element: <Auth />,
  children: [signUpRoute, signInRoute],
};

export { authRoute };
