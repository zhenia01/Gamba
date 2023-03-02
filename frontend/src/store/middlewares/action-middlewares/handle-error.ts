import { HttpStatusCode } from '@/common/enums';
import { openToast } from '@/components/common';
import { applyProxyOnActions } from '@/store/middlewares/action-middlewares/common/apply-proxy';
import { StoreActions } from '@/store/middlewares/action-middlewares/common/store-actions.type';

const handleError = <T extends StoreActions>(actions: T): T =>
  applyProxyOnActions(actions, async ([_, action]) => {
    try {
      await action();
    } catch (e) {
      if (
        e instanceof Error &&
        e.message.includes(HttpStatusCode.INTERNAL_SERVER_ERROR.toString())
      ) {
        openToast({
          title: e.name,
          description: e.message,
          status: 'error',
          duration: 9000,
          isClosable: true,
        });
      }
      throw e;
    }
  });

export { handleError };
