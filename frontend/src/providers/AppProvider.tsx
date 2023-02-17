import { ChakraProvider } from '@chakra-ui/react';

import { DarkModeProvider } from './DarkModeProvider';

type Props = {
  children: React.ReactNode;
};

export function AppProvider({ children }: Props) {
  return (
    <ChakraProvider>
      <DarkModeProvider>{children}</DarkModeProvider>
    </ChakraProvider>
  );
}
