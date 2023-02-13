import { Outlet } from 'react-router-dom';

function Dashboard() {
  return (
    <div>
      <h1>Protected Dashboard</h1>
      <Outlet />
    </div>
  );
}

export { Dashboard };
