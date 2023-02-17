import { AppProvider } from '@/providers';
import { AppRoutes } from '@/routing/routes';

export function App() {
  return (
    <AppProvider>
      <AppRoutes />
    </AppProvider>
  );
}
