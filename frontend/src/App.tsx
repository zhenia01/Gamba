import { AppProvider } from '@/providers/AppProvider';
import { AppRoutes } from '@/routes/AppRoutes';

import { Toast } from './components/common';

export function App() {
  return (
    <AppProvider>
      <AppRoutes />
      <Toast />
    </AppProvider>
  );
}
