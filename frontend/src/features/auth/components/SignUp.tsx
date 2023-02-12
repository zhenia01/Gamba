import { ActionFunction, Form, redirect } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { Button } from '@/components/common';
import { authActions } from '@/features/auth';
import { getFormDataObjectFromRequest, nameOf } from '@/utils';

import { SignUpRequestDto } from '../common/types';

const action: ActionFunction = async ({ request }) => {
  const { signUp } = authActions;
  const signUpDto = await getFormDataObjectFromRequest<SignUpRequestDto>(
    request,
  );
  await signUp(signUpDto);

  return redirect(AppRoute.HOME);
};

const SignUp = () => {
  return (
    <Form method="post">
      <label style={{ display: 'block' }}>
        Name:
        <input type="text" name={nameOf<SignUpRequestDto>('name')} />
      </label>
      <label style={{ display: 'block' }}>
        Password:
        <input type="password" name={nameOf<SignUpRequestDto>('password')} />
      </label>
      <Button type="submit" label="Sign up"></Button>
    </Form>
  );
};

export { action, SignUp };
