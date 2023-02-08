import { Button } from '@/components/button/Button';
import { Link } from '@/components/link/Link';
import { authActions, useCurrentUser } from '@/features/auth';

function Home() {
  const user = useCurrentUser();

  return (
    <div>
      <p>Hello, {user?.name ?? 'anonymous'}</p>
      <div>{!user && <Link to="/sign-in">Sign In</Link>}</div>
      <div>{!user && <Link to="/sign-up">Sign Up</Link>}</div>
      <div>
        {user && <Button onClick={authActions.signOut} label="Sign out"></Button>}
      </div>
    </div>
  );
}

export { Home };
