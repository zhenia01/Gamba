import { chakra, Link as ChakraLink } from '@chakra-ui/react';
import { ReactNode } from 'react';
import {
  Link as RouterLink,
  LinkProps as RouterLinkProps,
} from 'react-router-dom';

type LinkProps = {
  isExternal?: boolean;
  children: ReactNode;
} & RouterLinkProps;

const LinkWrapper = ({ children, isExternal, ...linkProps }: LinkProps) => {
  return (
    <ChakraLink as={RouterLink} isExternal={isExternal} {...linkProps}>
      {children}
    </ChakraLink>
  );
};
const Link = chakra(LinkWrapper);

export { type LinkProps, Link };
