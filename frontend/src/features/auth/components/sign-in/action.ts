import { ActionFunction, json, redirect } from 'react-router-dom';

import { AppRoute } from '@/common/enums';
import { HttpError } from '@/common/types';
import { authActions } from '@/features/auth';
import { getFormDataObjectFromRequest } from '@/utils';

import { SignInRequestDto } from '../../common/types';

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

export { action };
