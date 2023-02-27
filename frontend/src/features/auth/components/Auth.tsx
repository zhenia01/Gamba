import { Box, Card, CardBody } from '@chakra-ui/react';

import { AppRoute } from '@/common/enums';
import { NavTabData, NavTabs } from '@/components/common';

import { AuthTabIndex } from '../common/enums/auth-tab-index';

const authTabData: NavTabData[] = [
  {
    path: AppRoute.SIGN_IN,
    index: AuthTabIndex.SIGN_IN,
    label: 'Sign In',
  },
  {
    path: AppRoute.SIGN_UP,
    index: AuthTabIndex.SIGN_UP,
    label: 'Sign Up',
  },
];

function Auth() {
  return (
    <Box display="flex" alignItems="center" h="100vh" justifyContent="center">
      <Card
        size="md"
        maxW="md"
        align="center"
        justify="center"
        variant="elevated"
      >
        <CardBody>
          <NavTabs data={authTabData}></NavTabs>
        </CardBody>
      </Card>
    </Box>
  );
}

export { Auth };
