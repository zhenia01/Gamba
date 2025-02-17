import { Box } from '@chakra-ui/react';
import { NonIndexRouteObject, useRouteError } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { Link } from '@/components/common';
import { Root } from '@/pages/Root';

function ErrorElement() {
  const error = useRouteError();
  // eslint-disable-next-line no-console
  console.error(error);

  return (
    <Box w="100vw" h="100vh">
      Error occurred! Check console for details.
      <p>
        <Link to={AppRoute.HOME}>Go Home</Link>
      </p>
    </Box>
  );
}

const rootRoute: NonIndexRouteObject = {
  path: AppRoute.ROOT,
  element: <Root />,
  errorElement: <ErrorElement />,
};

export { rootRoute };
