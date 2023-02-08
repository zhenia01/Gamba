import { HttpStatusCode } from '@/common/enums';
import { HttpError } from '@/common/types/http/http-error';
import { authActions } from '@/features/auth';
import { applyProxyOnActions } from '@/store/middlewares/action-middlewares/common/apply-proxy';
import { StoreActions } from '@/store/middlewares/action-middlewares/common/store-actions.type';

const handleUnauthorized = <T extends StoreActions>(actions: T): T =>
    applyProxyOnActions(actions, async ([_, action]) => {
    try {
      await action();
    } catch (e) {
      if (e instanceof HttpError && e.details.status === HttpStatusCode.UNAUTHORIZED) {
        authActions.signOut();
      } else {
        throw e;
      }
    }
  });

export { handleUnauthorized };
