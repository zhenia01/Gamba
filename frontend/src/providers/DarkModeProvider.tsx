import { useColorMode } from '@chakra-ui/react';

type Props = {
  children: React.ReactNode;
};

export function DarkModeProvider({ children }: Props) {
  const { setColorMode } = useColorMode();

  setColorMode('dark');

  return <>{children}</>;
}
