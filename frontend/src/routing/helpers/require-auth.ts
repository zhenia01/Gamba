import { LoaderFunction, LoaderFunctionArgs, redirect } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { getAuthToken } from '@/features/auth';

function requireAuth(loader: LoaderFunction) {
  return function (args: LoaderFunctionArgs) {
    const token = getAuthToken();

    if (!token) {
      throw redirect(AppRoute.SIGN_IN);
    }

    return loader.call(loader, args);
  };
}

export { requireAuth };
