import { openToast } from '@/components/common';
import { getCurrentUser } from '@/features/auth';

export const showUnauthorizedToast = () => {
  const user = getCurrentUser();

  if (user) {
    openToast({
      title: 'User unauthorized',
      description: 'Please login to application again.',
      status: 'error',
    });
  }
};
