import { Link as ChakraUILink } from '@chakra-ui/react';
import { FC } from 'react';

type Props = {
  href: string;
  children: string
};

const Link: FC<Props> = ({ href, children }) => {
  return <ChakraUILink href={href}>{ children }</ChakraUILink>;
};

export { Link };
