import { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { useCurrentUser } from '@/features/auth';

type Props = {
  children: ReactNode;
};

function ProtectedRoute({ children }: Props) {
  const user = useCurrentUser();

  if (!user) {
    return <Navigate to={AppRoute.SIGN_IN} />;
  }

  return <>{children}</>;
}

export { ProtectedRoute };
