import { HttpError } from '@/common/types';
import { openToast } from '@/components/common';
import { applyProxyOnActions } from '@/store/middlewares/action-middlewares/common/apply-proxy';
import { StoreActions } from '@/store/middlewares/action-middlewares/common/store-actions.type';

const handleError = <T extends StoreActions>(actions: T): T =>
  applyProxyOnActions(actions, async ([_, action]) => {
    try {
      await action();
    } catch (e) {
      if (
        e instanceof HttpError &&
        e.details.status >= 500 &&
        e.details.status <= 599
      ) {
        openToast({
          title: e.details.title,
          description: e.details.detail,
          status: 'error',
        });
      }
      throw e;
    }
  });

export { handleError };
