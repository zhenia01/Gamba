import { Button } from '@chakra-ui/react';

import { AppRoute } from '@/common/enums';
import { Link } from '@/components/common';
import { authActions, useCurrentUser } from '@/features/auth';

function Home() {
  const user = useCurrentUser();

  return (
    <div>
      <p>Hello, {user?.name ?? 'anonymous'}</p>
      <div>{!user && <Link to={AppRoute.SIGN_IN}>Sign In</Link>}</div>
      <div>{!user && <Link to={AppRoute.SIGN_UP}>Sign Up</Link>}</div>
      <div>
        {user && <Button onClick={authActions.signOut}>Sign out</Button>}
      </div>
      <div>
        <Link to={AppRoute.DASHBOARD}>Go to protected dashboard</Link>
      </div>
    </div>
  );
}

export { Home };
