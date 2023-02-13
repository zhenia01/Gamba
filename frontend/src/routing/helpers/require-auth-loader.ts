import { LoaderFunction, LoaderFunctionArgs, redirect } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { getAuthToken } from '@/features/auth';

function requireAuthLoader(loader: LoaderFunction) {
  return function (args: LoaderFunctionArgs) {
    const token = getAuthToken();

    if (!token) {
      throw redirect(AppRoute.SIGN_IN);
    }

    return loader(args);
  };
}

export { requireAuthLoader };
