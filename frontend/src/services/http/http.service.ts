import { ContentType } from '@/common/enums';
import { HttpError } from '@/common/types/http/http-error';
import { getAuthToken } from '@/features/auth';

type HttpOptions = {
  contentType: ContentType;
  method: 'GET' | 'POST' | 'DELETE' | 'PUT' | 'PATCH';
  payload?: BodyInit | object;
  hasAuth: boolean;
  queryParams: Record<string, unknown>;
  headers?: Record<string, string>;
};

class Http {
  public static async load<T = unknown>(
    url: string,
    options: Partial<HttpOptions> = {},
  ): Promise<T> {
    const {
      method = 'GET',
      payload = undefined,
      contentType = 'application/json',
      hasAuth = true,
      queryParams,
    } = options;

    const body =
      typeof payload === 'object' && payload
        ? JSON.stringify(payload)
        : payload;

    const headers = this.getHeaders(contentType, hasAuth);

    try {
      const response = await fetch(
        this.getUrlWithQueryParams(url, queryParams),
        {
          method,
          headers,
          body,
        },
      );
      const res: Response = await this.checkStatus(response);

      return this.parseJSON<T>(res);
    } catch (err) {
      return this.throwError(err);
    }
  }

  private static getHeaders(
    contentType?: ContentType,
    hasAuth?: boolean,
  ): Headers {
    const headers = new Headers();

    if (contentType) {
      headers.append('content-type', contentType);
    }

    if (hasAuth) {
      const token = getAuthToken();
      headers.append('authorization', `Bearer ${token}`);
    }

    return headers;
  }

  private static getUrlWithQueryParams(
    url: string,
    queryParams?: Record<string, unknown>,
  ): string {
    if (!queryParams) {
      return url;
    }
    const query = new URLSearchParams(
      queryParams as Record<string, string>,
    ).toString();

    return `${url}?${query}`;
  }

  private static async checkStatus(response: Response): Promise<Response> {
    if (!response.ok) {
      throw new HttpError({
        details: await Http.parseJSON(response),
      });
    }

    return response;
  }

  private static async parseJSON<T>(response: Response): Promise<T> {
    return response.json();
  }

  private static throwError(err: unknown): never {
    throw err;
  }
}

export { Http };
