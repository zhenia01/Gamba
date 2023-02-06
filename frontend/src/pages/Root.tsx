import { useEffect } from 'react';
import { Outlet } from 'react-router-dom';

import { authActions, getAuthToken } from '@/features/auth';

let didInit = false;

function Root() {
  useEffect(() => {
    if (!didInit) {
      didInit = true;

      if (getAuthToken()) {
        authActions.loadCurrentUser();
      }
    }
  }, []);

  return (
    <Outlet/>
  );
}

export { Root };