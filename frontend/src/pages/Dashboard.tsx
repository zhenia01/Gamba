import { Outlet } from 'react-router-dom';

import { TagsList, useFavoriteTags } from '@/features/tags';

function Dashboard() {
  const tags = useFavoriteTags();

  return (
    <div>
      <h1>Protected Dashboard</h1>
      <p>Favorite tags: </p>
      <TagsList tags={tags} />
      <Outlet />
    </div>
  );
}

export { Dashboard };
