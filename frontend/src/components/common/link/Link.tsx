import { Link as ChakraLink } from '@chakra-ui/react';
import { ThemingProps } from '@chakra-ui/system';
import {
  Link as RouterLink,
  LinkProps as RouterLinkProps,
} from 'react-router-dom';

type Props = {
  to: string;
  children: string;
} & ThemingProps<'Link'> &
  RouterLinkProps;

const Link = ({ to, children, ...linkProps }: Props) => {
  return (
    <ChakraLink as={RouterLink} to={to} {...linkProps}>
      {children}
    </ChakraLink>
  );
};

export { Link };
