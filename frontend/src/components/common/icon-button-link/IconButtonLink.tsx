import { IconButton, IconButtonProps } from '@chakra-ui/react';
import { ReactElement } from 'react';
import { To } from 'react-router-dom';

import { Link, LinkProps } from '../link/Link';

type Props = {
  to: To;
  isDisabled?: boolean;
  ariaLabel: string;
  linkProps?: Omit<LinkProps, 'to' | 'children'>;
  buttonProps?: Omit<IconButtonProps, 'aria-label'>;
  children: ReactElement;
};

const IconButtonLink = ({
  to,
  isDisabled,
  linkProps,
  buttonProps,
  children,
  ariaLabel,
}: Props) => {
  return (
    <Link to={to} {...linkProps} _hover={{ all: 'unset' }}>
      <IconButton
        aria-label={ariaLabel}
        isDisabled={isDisabled}
        {...buttonProps}
      >
        {children}
      </IconButton>
    </Link>
  );
};

export { IconButtonLink };
