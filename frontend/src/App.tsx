import { AppProvider } from '@/providers';
import { AppRoutes } from '@/routing/routes';

import { Toast } from './components/common';

export function App() {
  return (
    <AppProvider>
      <AppRoutes />
      <Toast />
    </AppProvider>
  );
}
