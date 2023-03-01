import {
  extendTheme,
  ThemeConfig,
  withDefaultColorScheme,
} from '@chakra-ui/react';

import { components } from './components';

const config: ThemeConfig = {
  initialColorMode: 'dark',
  useSystemColorMode: false,
};

const theme = extendTheme(
  { config, components },
  withDefaultColorScheme({ colorScheme: 'teal' }),
);
export { theme };
