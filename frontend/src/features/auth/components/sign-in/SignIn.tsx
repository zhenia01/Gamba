import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
} from '@chakra-ui/react';
import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { Form, useActionData, useNavigation } from 'react-router-dom';

import { HttpStatusCode } from '@/common/enums';
import { ProblemDetails } from '@/common/types/http/http-error';

import { SignInRequestDto } from '../../common/types';
import { nameValidation } from '../../common/validations/name.validation';
import { passwordValidation } from '../../common/validations/password.validation';
import { PasswordInput } from '../common/PasswordInput';

enum UserErrors {
  WrongUsername = 'wrongUsername',
  WrongPassword = 'wrongPassword',
}

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

export { SignIn };
