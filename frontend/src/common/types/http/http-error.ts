import { HttpStatusCode } from '@/common/enums';
import { ErrorType } from '@/common/enums/errors/errors';

type ProblemDetails = {
  status: HttpStatusCode;
  title: string;
  detail?: string;
  type?: string;
  instance?: string;
  error: ErrorType;
} & Record<string, unknown>;

type Constructor = {
  message?: string;
  details: ProblemDetails;
};

class HttpError extends Error {
  public details: ProblemDetails;

  public constructor({ details, message }: Constructor) {
    super(
      message ??
        `${details.status}: ${details.title}\n Details: ${
          details.detail ?? 'none'
        }`,
    );
    this.details = details;
  }
}

export { type ProblemDetails, HttpError };
