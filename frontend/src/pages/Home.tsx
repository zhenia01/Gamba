import { Link } from 'react-router-dom';

import { authActions, useCurrentUser } from '@/features/auth';

function Home() {
  const user = useCurrentUser();

  return (
    <div>
      <p>Hello, {user?.name ?? 'anonymous'}</p>
      <div>{!user && <Link to="/sign-in">Sign In</Link>}</div>
      <div>{!user && <Link to="/sign-up">Sign Up</Link>}</div>
      <div>
        {user && <button onClick={authActions.signOut}>Sign out</button>}
      </div>
    </div>
  );
}

export { Home };
