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

import { ProblemDetails } from '@/common/types/http/http-error';

import { SignUpRequestDto } from '../../common/types';
import { nameValidation } from '../../common/validations/name.validation';
import { passwordValidation } from '../../common/validations/password.validation';
import { PasswordInput } from '../common/PasswordInput';

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
        isRequired={nameValidation.required}
        isInvalid={!!nameErrors}
      >
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
        Sign up
      </Button>
    </Form>
  );
};

export { SignUp };
