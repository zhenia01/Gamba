import { Button, ButtonProps } from '@chakra-ui/react';
import { ReactNode } from 'react';
import { To } from 'react-router-dom';

import { Link, LinkProps } from '../link/Link';

type Props = {
  to: To;
  isDisabled?: boolean;
  linkProps?: Omit<LinkProps, 'to' | 'children'>;
  buttonProps?: ButtonProps;
  children: ReactNode;
};

const ButtonLink = ({
  to,
  isDisabled,
  linkProps,
  buttonProps,
  children,
}: Props) => {
  return (
    <Link to={to} {...linkProps} _hover={{ all: 'unset' }}>
      <Button isDisabled={isDisabled} {...buttonProps}>
        {children}
      </Button>
    </Link>
  );
};

export { ButtonLink };
