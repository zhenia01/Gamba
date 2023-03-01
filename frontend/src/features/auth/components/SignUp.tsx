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

import { AppRoute } from '@/common/enums';
import { HttpError } from '@/common/types';
import { ProblemDetails } from '@/common/types/http/http-error';
import { authActions } from '@/features/auth';
import { getFormDataObjectFromRequest } from '@/utils';

import { SignUpRequestDto } from '../common/types';
import { nameValidation } from '../common/validations/name.validation';
import { passwordValidation } from '../common/validations/password.validation';
import { PasswordInput } from './PasswordInput';

const action: ActionFunction = async ({ request }) => {
  try {
    const signUpDto = await getFormDataObjectFromRequest<SignUpRequestDto>(
      request,
    );
    await authActions.signUp(signUpDto);

    return redirect(AppRoute.HOME);
  } catch (e) {
    if (e instanceof HttpError) {
      return json(e.details);
    }
    throw e;
  }
};

const enum NameError {
  Unique = 'unique',
}

const SignUp = () => {
  const { state: navigationState } = useNavigation();
  const actionData = useActionData() as ProblemDetails;
  const {
    register,
    setError,
    clearErrors,
    formState: {
      isValid,
      errors: { name: nameErrors, password: passwordErrors },
    },
  } = useForm<SignUpRequestDto>();

  useEffect(() => {
    if (actionData?.error === 'UserNameMustBeUnique') {
      setError(
        'name',
        { type: NameError.Unique, message: actionData.detail },
        { shouldFocus: true },
      );
    }
  }, [actionData]);

  const isNameTaken = nameErrors?.type === NameError.Unique;

  return (
    <Form method="post">
      <FormControl
        isRequired={!!nameValidation.required}
        isInvalid={!!nameErrors}
      >
        <FormLabel>Name</FormLabel>
        <Input
          type="text"
          placeholder="Name"
          {...register('name', {
            ...nameValidation,
            onChange: function () {
              if (isNameTaken) clearErrors('name');
            },
          })}
        />
        {nameErrors && (
          <FormErrorMessage>{nameErrors.message}</FormErrorMessage>
        )}
      </FormControl>
      <FormControl
        isRequired={!!passwordValidation.required}
        isInvalid={!!passwordErrors}
      >
        <FormLabel>Password</FormLabel>
        <PasswordInput
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
        Sign up
      </Button>
    </Form>
  );
};

export { action, SignUp };
