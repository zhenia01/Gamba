import { User } from './user';

type SignUpResponseDto = {
  token: string;
  user: User;
};

export { type SignUpResponseDto };
