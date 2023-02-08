import { StoreActions } from '@/store/middlewares/action-middlewares/common/store-actions.type';

function applyProxyOnActions<T extends StoreActions>(
  actions: T,
  proxiedApply: ([name, action]: [string, CallableFunction]) => void,
): T {
  const proxiedActions = Object.entries(actions).map(([name, action]) => [
    name,
    new Proxy(action, {
      // eslint-disable-next-line max-params
      async apply(target, thisArg, args) {
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-ignore
        await proxiedApply([name, () => target.apply(thisArg, args)]);
      },
    }),
  ]);

  return Object.fromEntries(proxiedActions) as T;
}

export { applyProxyOnActions };
