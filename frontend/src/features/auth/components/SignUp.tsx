import { ActionFunction, Form, redirect } from 'react-router-dom';

import { getFormDataObjectFromRequest, nameOf } from '@/utils';

import { SignUpRequestDto } from '../common/types';
import { authActions } from '../store/auth-store';

const { signUp } = authActions;

const action: ActionFunction = async ({ request }) => {
  const signUpDto = await getFormDataObjectFromRequest<SignUpRequestDto>(request);
  await signUp(signUpDto);

  return redirect('/home');
};

const SignUp = () => {
  return <Form method="post">
    <label>
      Name:{+' '}
      <input type="text" name={nameOf<SignUpRequestDto>('name')}/>
    </label>
    <label>
      Password:{+' '}
      <input type="password" name={nameOf<SignUpRequestDto>('password')}/>
    </label>
    <button type="submit">Sign up</button>
  </Form>;
};

export { action, SignUp };