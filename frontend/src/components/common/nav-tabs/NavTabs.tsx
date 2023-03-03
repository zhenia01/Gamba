import './NavTabs.scss';

import { Tab, TabList, TabPanels, Tabs } from '@chakra-ui/react';
import { Link, Outlet, useLocation } from 'react-router-dom';

import type { NavTabData } from './interfaces/nav-tab-data';
import { getCurrentTabIndex } from './utils/get-current-tab-index';

type Props = {
  data: NavTabData[];
};

export function NavTabs({ data }: Props) {
  const location = useLocation();
  const currentTabIndex = getCurrentTabIndex(location.pathname, data);

  return (
    <Tabs index={currentTabIndex}>
      <TabList>
        {data.map(({ label, index, path }) => (
          <Tab as={Link} to={path} key={index}>
            {label}
          </Tab>
        ))}
      </TabList>
      <TabPanels pt="10px">
        <Outlet />
      </TabPanels>
    </Tabs>
  );
}
