import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import { authRoute } from '@/features/auth';

import { dashboardRoute } from './dashboard/dashboard.route';
import { homeRoute } from './home/home.route';
import { rootRoute } from './root/root.route';
import { rootIndexRoute } from './root/root-index.route';

const router = createBrowserRouter([
  {
    ...rootRoute,
    children: [rootIndexRoute, authRoute, homeRoute, dashboardRoute],
  },
]);

export function AppRoutes() {
  return <RouterProvider router={router} />;
}
