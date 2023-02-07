import { ActionFunction, Form, redirect } from 'react-router-dom';

import { Button } from '@/components/button/Button';
import { authActions } from '@/features/auth';
import { getFormDataObjectFromRequest, nameOf } from '@/utils';

import { SignInRequestDto } from '../common/types';

const action: ActionFunction = async ({ request }) => {
  const { signIn } = authActions;
  const signInDto = await getFormDataObjectFromRequest<SignInRequestDto>(
    request,
  );
  await signIn(signInDto);

  return redirect('/home');
};

const SignIn = () => {
  return (
    <Form method="post">
      <label style={{ display: 'block' }}>
        Name:
        <input type="text" name={nameOf<SignInRequestDto>('name')} />
      </label>
      <label style={{ display: 'block' }}>
        Password:
        <input type="password" name={nameOf<SignInRequestDto>('password')} />
      </label>
      <Button type="submit" label="Sign in"></Button>
    </Form>
  );
};

export { action, SignIn };
