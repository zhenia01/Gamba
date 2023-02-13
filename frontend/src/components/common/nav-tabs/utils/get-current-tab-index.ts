import type { NavTabData } from '../interfaces/nav-tab-data';

export function getCurrentTabIndex(
  currentLocation: string,
  data: NavTabData[],
): number | undefined {
  const currentTabData = data.find(({ path }) => path === currentLocation);

  return currentTabData?.index;
}
