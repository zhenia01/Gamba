import { ENV } from '@/common/enums';
import { Http } from '@/services/http/http.service';

import { SignInRequestDto, SignInResponseDto, SignUpRequestDto, SignUpResponseDto, User } from '../common/types';

class AuthApi {
  #authApiRoute = `${ENV.API_PATH}/auth`;

  public signUp(payload: SignUpRequestDto): Promise<SignUpResponseDto> {
    return Http.load(
      `${this.#authApiRoute}/sign-up`,
      {
        method: 'POST',
        payload: payload,
        hasAuth: false,
      },
    );
  }

  public signIn(payload: SignInRequestDto): Promise<SignInResponseDto> {
    return Http.load(
      `${this.#authApiRoute}/sign-in`,
      {
        method: 'POST',
        payload: payload,
        hasAuth: false,
      },
    );
  }

  public getCurrentUser(): Promise<User> {
    return Http.load(
      `${this.#authApiRoute}/current-user`,
      {
        method: 'GET',
      },
    );
  }
}

const authApi = new AuthApi();

export { authApi };
