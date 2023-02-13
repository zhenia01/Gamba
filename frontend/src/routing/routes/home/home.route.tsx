import React from 'react';
import { NonIndexRouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { Home } from '@/pages';

const homeRoute: NonIndexRouteObject = {
  path: AppRoute.HOME,
  element: <Home />,
};

export { homeRoute };
