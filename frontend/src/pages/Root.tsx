import { Outlet } from 'react-router-dom';

import { authActions, getAuthToken } from '@/features/auth';

function loader() {
  const token = getAuthToken();

  if (token) {
    authActions.updateCurrentUser();
  }
}

function Root() {
  return (
    <Outlet/>
  );
}

export { Root, loader as rootLoader };