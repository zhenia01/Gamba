import {
  Button,
  Center,
  Flex,
  Grid,
  GridItem,
  Image,
  Link,
  Spacer,
  Text,
} from '@chakra-ui/react';
import { useEffect } from 'react';
import { Outlet } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { ButtonLink, IconButtonLink } from '@/components/common';
import { authActions, getAuthToken, useCurrentUser } from '@/features/auth';

let didInit = false;

function Root() {
  const user = useCurrentUser();

  useEffect(() => {
    if (!didInit) {
      didInit = true;

      if (getAuthToken()) {
        authActions.loadCurrentUser();
      }
    }
  }, []);

  return (
    <Grid minH="100vh" gridTemplate="auto 1fr auto / auto 1fr">
      <GridItem as="header" gridColumn="1 / 3" h="60px" p="10px">
        <Flex h="100%" gap="2" alignItems="center">
          <IconButtonLink
            to={AppRoute.HOME}
            buttonProps={{ variant: 'unstyled' }}
            ariaLabel="Main logo"
          >
            <Image src="/logo-text.svg" h="100%" />
          </IconButtonLink>
          <Spacer />
          <p>Hello, {user?.name ?? 'anonymous'}</p>
          <Spacer />
          {!user && <ButtonLink to={AppRoute.SIGN_IN}>Sign In</ButtonLink>}
          {!user && <ButtonLink to={AppRoute.SIGN_UP}>Sign Up</ButtonLink>}
          <div>
            {user && <Button onClick={authActions.signOut}>Sign out</Button>}
          </div>
        </Flex>
      </GridItem>
      <GridItem as="aside" gridColumn="1 / 2">
        {/*Following list*/}
      </GridItem>
      <GridItem as="main" gridColumn="2 / 3" p="10px">
        <Outlet />
      </GridItem>
      <GridItem as="footer" gridColumn="1 / 3" h="30px">
        <Center>
          <Text>
            Made by{' '}
            <Link isExternal href="https://github.com/moran711">
              Winston
            </Link>{' '}
            and{' '}
            <Link isExternal href="https://github.com/zhenia01">
              Lays
            </Link>
          </Text>
        </Center>
      </GridItem>
    </Grid>
  );
}

export { Root };
