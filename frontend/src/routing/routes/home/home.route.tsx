import React from 'react';
import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { Home } from '@/pages';

const homeRoute: RouteObject = {
  path: AppRoute.HOME,
  element: <Home />,
};

export { homeRoute };
