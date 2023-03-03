import { ChakraProvider } from '@chakra-ui/react';
import { ReactNode } from 'react';

import { theme } from '@/theme';

type Props = {
  children: ReactNode;
};

export function AppProvider({ children }: Props) {
  return <ChakraProvider theme={theme}>{children}</ChakraProvider>;
}
