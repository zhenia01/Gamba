import { RouteObject } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { Link } from '@/components/common';
import { Root } from '@/pages';

const rootRoute: RouteObject = {
  path: AppRoute.ROOT,
  element: <Root />,
  errorElement: (
    <div>
      Error occurred!
      <Link to={AppRoute.HOME}>Go Home</Link>
    </div>
  ),
};

export { rootRoute };
