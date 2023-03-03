import { NonIndexRouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { ProtectedRoute } from '@/components/common';
import { Home } from '@/pages/Home';

const homeRoute: NonIndexRouteObject = {
  path: AppRoute.HOME,
  element: (
    <ProtectedRoute>
      <Home />
    </ProtectedRoute>
  ),
};

export { homeRoute };
