import { Button as ChakraUIButton } from '@chakra-ui/react';

type Props = {
  label: string;
  variant?: 'ghost' | 'outline' | 'solid' | 'link' | 'unstyled';
  type?: 'button' | 'submit';
  onClick?: () => void;
};

const Button = ({
  variant = 'solid',
  label,
  type = 'button',
  onClick,
}: Props) => {
  return (
    <ChakraUIButton
      colorScheme="teal"
      variant={variant}
      type={type}
      onClick={onClick}
    >
      {label}
    </ChakraUIButton>
  );
};

export { Button };
