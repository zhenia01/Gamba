import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { ProtectedRoute } from '@/components/common';
import { Dashboard } from '@/pages';

const dashboardRoute: RouteObject = {
  path: AppRoute.DASHBOARD,
  element: (
    <ProtectedRoute>
      <Dashboard />
    </ProtectedRoute>
  ),
};

export { dashboardRoute };
