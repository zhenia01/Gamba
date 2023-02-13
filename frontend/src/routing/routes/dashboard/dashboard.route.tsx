import { NonIndexRouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { ProtectedRoute } from '@/components/common';
import { Dashboard } from '@/pages';

const dashboardRoute: NonIndexRouteObject = {
  path: AppRoute.DASHBOARD,
  element: (
    <ProtectedRoute>
      <Dashboard />
    </ProtectedRoute>
  ),
};

export { dashboardRoute };
