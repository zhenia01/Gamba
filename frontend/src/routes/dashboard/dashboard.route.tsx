import { NonIndexRouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { ProtectedRoute } from '@/components/common';
import { tagsActions } from '@/features/tags';
import { Dashboard } from '@/pages/Dashboard';

const dashboardRoute: NonIndexRouteObject = {
  loader: async () => {
    await tagsActions.getFavoriteTags();

    return null;
  },
  path: AppRoute.DASHBOARD,
  element: (
    <ProtectedRoute>
      <Dashboard />
    </ProtectedRoute>
  ),
};

export { dashboardRoute };
