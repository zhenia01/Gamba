import { Link } from 'react-router-dom';

import { useCurrentUser } from '@/features/auth';

function Home() {
  const user = useCurrentUser();

  return(
  <div>
    <p>Hello, {user?.name ?? 'anonymous'}</p>
    {!user && <Link to="/sign-up" >Sign Up</Link>}
  </div>);
}

export { Home };