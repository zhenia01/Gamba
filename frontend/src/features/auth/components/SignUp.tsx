import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  InputGroup,
  InputRightElement,
  useBoolean,
} from '@chakra-ui/react';
import React, { useEffect } from 'react';
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

const nameValidation = {
  maxLength: 15,
  minLength: 5,
  required: true,
};
const passwordValidation = {
  minLength: 5,
  required: true,
};

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
  const [isPasswordHidden, { toggle }] = useBoolean(true);

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
        <InputGroup>
          <Input
            placeholder="Password"
            {...passwordValidation}
            type={isPasswordHidden ? 'password' : 'text'}
            {...register('password', {
              ...passwordValidation,
            })}
          />
          <InputRightElement>
            <Button variant="outline" onClick={toggle}>
              {isPasswordHidden ? 'Show' : 'Hide'}
            </Button>
          </InputRightElement>
        </InputGroup>
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
