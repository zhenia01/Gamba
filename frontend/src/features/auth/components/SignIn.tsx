import { ActionFunction, Form, redirect } from 'react-router-dom';

import { getFormDataObjectFromRequest, nameOf } from '@/utils';

import { SignInRequestDto } from '../common/types';
import { authActions } from '../store/auth-store';

const { signIn } = authActions;

const action: ActionFunction = async ({ request }) => {
  const signInDto = await getFormDataObjectFromRequest<SignInRequestDto>(request);
  await signIn(signInDto);

  return redirect('/home');
};

const SignIn = () => {
  return <Form method="post">
    <label>
      Name:{+' '}
      <input type="text" name={nameOf<SignInRequestDto>('name')}/>
    </label>
    <label>
      Password:{+' '}
      <input type="password" name={nameOf<SignInRequestDto>('password')}/>
    </label>
    <button type="submit">Sign in</button>
  </Form>;
};

export { action, SignIn };