import { ENV } from '@/common/enums';
import { Http } from '@/services/http/http.service';

import { Tag } from '../common/types/tag';

class TagsApi {
  #tagsApiRoute = `${ENV.API_PATH}/user/tags`;

  public updateFavoriteTags(tags: Tag[]): Promise<Tag[]> {
    return Http.load(`${this.#tagsApiRoute}/favorite`, {
      method: 'PUT',
      payload: { tags },
      hasAuth: true,
    });
  }

  public updateCreatorTags(tags: Tag[]): Promise<Tag[]> {
    return Http.load(`${this.#tagsApiRoute}/creator`, {
      method: 'PUT',
      payload: { tags },
      hasAuth: true,
    });
  }

  public getFavoriteTags(): Promise<Tag[]> {
    return Http.load(`${this.#tagsApiRoute}/favorite`, {
      method: 'GET',
      hasAuth: true,
    });
  }

  public getCreatorTags(): Promise<Tag[]> {
    return Http.load(`${this.#tagsApiRoute}/creator`, {
      method: 'GET',
      hasAuth: true,
    });
  }
}

const tagsApi = new TagsApi();

export { tagsApi };
