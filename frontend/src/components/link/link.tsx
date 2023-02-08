import { Link as ChakraUILink } from '@chakra-ui/react';
import { Link as ReactLink } from 'react-router-dom';

type Props = {
  to: string;
  children: string;
};

const Link = ({ to, children }: Props) => {
  return (
    <ChakraUILink as={ReactLink} to={to}>
      {children}
    </ChakraUILink>
  );
};

export { Link };
