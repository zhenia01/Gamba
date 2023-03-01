import { Button, FormControl, FormLabel, Input } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import {
  ActionFunction,
  Form,
  json,
  redirect,
  useNavigation,
} from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { HttpError } from '@/common/types';
import { authActions } from '@/features/auth';
import { getFormDataObjectFromRequest } from '@/utils';

import { SignInRequestDto } from '../common/types';
import { nameValidation } from '../common/validations/name.validation';
import { passwordValidation } from '../common/validations/password.validation';
import { PasswordInput } from './PasswordInput';

const action: ActionFunction = async ({ request }) => {
  try {
    const { signIn } = authActions;
    const signInDto = await getFormDataObjectFromRequest<SignInRequestDto>(
      request,
    );
    await signIn(signInDto);

    return redirect(AppRoute.HOME);
  } catch (e) {
    if (e instanceof HttpError) {
      return json(e.details);
    }
    throw e;
  }
};

const SignIn = () => {
  const { state: navigationState } = useNavigation();

  const {
    register,
    formState: { isValid },
  } = useForm<SignInRequestDto>();

  return (
    <Form method="post">
      <FormControl isRequired>
        <FormLabel>Name</FormLabel>
        <Input
          type="text"
          placeholder="Name"
          {...nameValidation}
          {...register('name', nameValidation)}
        />
      </FormControl>
      <FormControl isRequired>
        <FormLabel>Password</FormLabel>
        <PasswordInput
          {...passwordValidation}
          {...register('password', {
            ...passwordValidation,
          })}
        />
      </FormControl>
      <Button
        type="submit"
        isLoading={navigationState == 'submitting'}
        isDisabled={!isValid}
      >
        Sign in
      </Button>
    </Form>
  );
};

export { action, SignIn };
