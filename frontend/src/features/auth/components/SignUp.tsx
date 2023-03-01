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

const enum NameErrors {
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
      errors: { name: nameErrors },
    },
  } = useForm<SignUpRequestDto>();

  useEffect(() => {
    if (actionData?.error === 'UserNameMustBeUnique') {
      setError(
        'name',
        { type: NameErrors.Unique, message: actionData.detail },
        { shouldFocus: true },
      );
    }
  }, [actionData]);

  const isNameTaken = nameErrors?.type === NameErrors.Unique;

  return (
    <Form method="post">
      <FormControl isRequired isInvalid={!!nameErrors}>
        <FormLabel>Name</FormLabel>
        <Input
          type="text"
          placeholder="Name"
          {...nameValidation}
          {...register('name', {
            ...nameValidation,
            onChange: function () {
              if (isNameTaken) clearErrors('name');
            },
          })}
        />
        <FormErrorMessage>{nameErrors?.message?.toString()}</FormErrorMessage>
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
        Sign up
      </Button>
    </Form>
  );
};

export { action, SignUp };
