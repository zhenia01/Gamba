import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';
import {
  IconButton,
  Input,
  InputGroup,
  InputProps,
  InputRightElement,
  useBoolean,
} from '@chakra-ui/react';
import { ForwardedRef, forwardRef } from 'react';

type Props = InputProps;

const PasswordInput = forwardRef<HTMLInputElement, Props>(
  ({ ...inputProps }: Props, inputRef: ForwardedRef<HTMLInputElement>) => {
    const [isPasswordHidden, { toggle: togglePasswordHidden }] =
      useBoolean(true);

    return (
      <InputGroup>
        <Input
          placeholder="Password"
          type={isPasswordHidden ? 'password' : 'text'}
          ref={inputRef}
          {...inputProps}
        />
        <InputRightElement>
          <IconButton
            variant="ghost"
            onClick={togglePasswordHidden}
            icon={isPasswordHidden ? <ViewIcon /> : <ViewOffIcon />}
            aria-label="Toggle password hidden state"
          />
        </InputRightElement>
      </InputGroup>
    );
  },
);

export { PasswordInput };
