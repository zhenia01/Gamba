import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
} from '@chakra-ui/react';
import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import {
  ActionFunction,
  Form,
  json,
  redirect,
  useActionData,
  useNavigation,
} from 'react-router-dom';

import { AppRoute, HttpStatusCode } from '@/common/enums';
import { HttpError } from '@/common/types';
import { ProblemDetails } from '@/common/types/http/http-error';
import { authActions } from '@/features/auth';
import { getFormDataObjectFromRequest } from '@/utils';

import { SignInRequestDto } from '../common/types';
import { nameValidation } from '../common/validations/name.validation';
import { passwordValidation } from '../common/validations/password.validation';
import { PasswordInput } from './PasswordInput';

enum UserErrors {
  WrongUsername = 'wrongUsername',
  WrongPassword = 'wrongPassword',
}

const action: ActionFunction = async ({ request }) => {
  try {
    const signInDto = await getFormDataObjectFromRequest<SignInRequestDto>(
      request,
    );
    await authActions.signIn(signInDto);

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
  const actionData = useActionData() as ProblemDetails;

  const {
    register,
    setError,
    formState: {
      isValid,
      errors: { name: nameErrors, password: passwordErrors },
    },
  } = useForm<SignInRequestDto>({ mode: 'all' });

  useEffect(() => {
    if (actionData?.status === HttpStatusCode.NOT_FOUND) {
      setError('name', {
        type: UserErrors.WrongUsername,
        message: actionData.detail,
      });
    } else if (actionData?.status === HttpStatusCode.UNAUTHORIZED) {
      setError('password', {
        type: UserErrors.WrongPassword,
        message: actionData.detail,
      });
    }
  }, [actionData]);

  return (
    <Form method="post">
      <FormControl
        isRequired={nameValidation.required}
        isInvalid={!!nameErrors}
      >
        <FormLabel>Name</FormLabel>
        <Input
          type="text"
          placeholder="Name"
          {...nameValidation}
          {...register('name', nameValidation)}
        />
        <FormErrorMessage>{nameErrors?.message}</FormErrorMessage>
      </FormControl>
      <FormControl
        isRequired={passwordValidation.required}
        isInvalid={!!passwordErrors}
      >
        <FormLabel>Password</FormLabel>
        <PasswordInput
          {...passwordValidation}
          {...register('password', {
            ...passwordValidation,
          })}
        />
        <FormErrorMessage>{passwordErrors?.message}</FormErrorMessage>
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
