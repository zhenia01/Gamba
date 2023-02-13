import './NavTabs.scss';

import { Tab, TabList, TabPanels, Tabs } from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';

import { noop } from '@/common/utils/noop';

import type { NavTabData } from './interfaces/nav-tab-data';
import { getCurrentTabIndex } from './utils/get-current-tab-index';

type Props = {
  data: NavTabData[];
};

export function NavTabs({ data }: Props) {
  const [currentTabIndex, setCurrentTabIndex] = useState<number>();
  const location = useLocation();

  useEffect(() => {
    const tabIndex = getCurrentTabIndex(location.pathname, data);
    setCurrentTabIndex(tabIndex);
  }, [location]);

  return (
    <Tabs index={currentTabIndex} onChange={noop}>
      <TabList>
        {data.map(({ label, index, path }) => (
          <Tab as={Link} to={path} key={index}>
            {label}
          </Tab>
        ))}
      </TabList>
      <TabPanels>
        <Outlet />
      </TabPanels>
    </Tabs>
  );
}
