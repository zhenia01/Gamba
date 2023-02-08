import { HttpStatusCode } from '@/common/enums';
import { HttpError } from '@/common/types/http/http-error';
import { authActions } from '@/features/auth';

type StoreActions = { [k: string]: CallableFunction };

function handleUnauthorized<T extends StoreActions>(actions: T): T {
  const proxiedActions = Object.entries(actions).map(([name, action]) => [
    name,
    new Proxy(action, {
      // eslint-disable-next-line max-params
      async apply(target, thisArg, args) {
        try {
          // eslint-disable-next-line @typescript-eslint/ban-ts-comment
          // @ts-ignore
          await target.apply(thisArg, args);
        } catch (e) {
          if (
            e instanceof HttpError &&
            e.details.status === HttpStatusCode.UNAUTHORIZED
          ) {
            // eslint-disable-next-line no-console
            console.error(e.message); // TODO: replace with toastr
            authActions.signOut();
          } else {
            throw e;
          }
        }
      },
    }),
  ]);

  return Object.fromEntries(proxiedActions) as T;
}

export { handleUnauthorized };
