import { createStandaloneToast, UseToastOptions } from '@chakra-ui/toast';

const { ToastContainer, toast } = createStandaloneToast();

function Toast() {
  return <ToastContainer />;
}

const openToast = (options: UseToastOptions) =>
  toast({
    duration: 9000,
    isClosable: true,
    ...options,
  });

export { openToast, Toast };
