import { ChakraProvider } from '@chakra-ui/react';

import { theme } from '@/theme';

type Props = {
  children: React.ReactNode;
};

export function AppProvider({ children }: Props) {
  return <ChakraProvider theme={theme}>{children}</ChakraProvider>;
}
