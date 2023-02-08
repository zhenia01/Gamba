import { applyProxyOnActions } from '@/store/middlewares/action-middlewares/common/apply-proxy';
import { StoreActions } from '@/store/middlewares/action-middlewares/common/store-actions.type';

const handleError = <T extends StoreActions>(actions: T): T =>
  applyProxyOnActions(actions, async ([name, action]) => {
    try {
      await action();
    } catch (e) {
      if (e instanceof Error) {
        // eslint-disable-next-line no-console
        console.error(`Error in action [${name}]: ${e.message}`); // TODO: replace with toastr
      }
    }
  });

export { handleError };