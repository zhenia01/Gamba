import { ContentType } from '@/common/enums';
import { getAuthToken } from '@/features/auth';

type HttpOptions = {
  contentType: ContentType,
  method: 'GET' | 'POST' | 'DELETE' | 'PUT' | 'PATCH',
  payload?: BodyInit | object,
  hasAuth: boolean,
  queryParams: Record<string, unknown>,
  headers?: Record<string, string>
};

class Http {
  public static load<T = unknown>(
    url: string,
    options: Partial<HttpOptions> = {},
  ): Promise<T> {
    const {
      method = 'GET',
      payload = null,
      contentType = 'application/json',
      hasAuth = true,
      queryParams,
    } = options;

    const body = (typeof payload === 'object') ? JSON.stringify(payload) : payload;
    const headers = this.getHeaders(contentType, hasAuth);

    return fetch(this.getUrlWithQueryParams(url, queryParams), {
      method,
      headers,
      body,
    })
      .then(this.checkStatus)
      .then((res) => this.parseJSON<T>(res))
      .catch(this.throwError);
  }

  private static getHeaders(contentType?: ContentType, hasAuth?: boolean): Headers {
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
      const parsedException = await response.json().catch(() => ({
        message: response.statusText,
      }));

      throw new Error(`${response.status} ${parsedException.message}`);
    }

    return response;
  }

  private static async parseJSON<T>(response: Response): Promise<T> {
    return response.json();
  }

  private static throwError(err: Error): never {
    throw err;
  }
}

export { Http };
