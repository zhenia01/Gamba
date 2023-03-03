import React from 'react';
import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { Link } from '@/components/common';

const rootIndexRoute: RouteObject = {
  index: true,
  element: (
    <p>
      <Link to={AppRoute.HOME}>Go Home</Link>
    </p>
  ),
};

export { rootIndexRoute };
