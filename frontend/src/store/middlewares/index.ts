import { StoreActions } from '@/store/middlewares/action-middlewares/common/store-actions.type';

import { handleError } from './action-middlewares/handle-error';
import { handleUnauthorized } from './action-middlewares/handle-unauthorized';

const handleHttpCalls = <T extends StoreActions>(actions: T) => handleError(handleUnauthorized(actions));

export { handleHttpCalls } ;
