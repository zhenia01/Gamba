import { User } from './user';

type SignInResponseDto = {
  token: string,
  user: User
};

export { type SignInResponseDto };