import { Box, Card, CardBody, CardHeader, Image } from '@chakra-ui/react';

import { AppRoute } from '@/common/enums';
import { NavTabData, NavTabs } from '@/components/common';

import { AuthTabIndex } from '../common/enums/auth-tab-index.enum';

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
      <Card align="center" justify="center" variant="elevated" w="40%" p="20px">
        <CardHeader>
          <Image src="/logo-text.svg" boxSize="70%" margin="auto" />
        </CardHeader>
        <CardBody w="80%">
          <NavTabs data={authTabData}></NavTabs>
        </CardBody>
      </Card>
    </Box>
  );
}

export { Auth };
