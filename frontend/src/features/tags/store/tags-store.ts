import { create } from 'zustand';

import { handleHttpCalls } from '@/store/middlewares';

import { Tag } from '../common/types';
import { tagsApi } from '../services/tags-api.service';

type TagsState = {
  favoriteTags: Tag[];
  creatorTags?: Tag[];
};

const initialState: TagsState = {
  favoriteTags: [],
  creatorTags: undefined,
};

type TagsActions = {
  getFavoriteTags: () => Promise<void>;
  getCreatorTags: () => Promise<void>;
  updateFavoriteTags: (request: Tag[]) => Promise<void>;
  updateCreatorTags: (request: Tag[]) => Promise<void>;
};

const useTagsStore = create<TagsState>()(() => ({ ...initialState }));

const tagsActions = handleHttpCalls<TagsActions>({
  getFavoriteTags: async () => {
    const tags = await tagsApi.getFavoriteTags();
    useTagsStore.setState({ favoriteTags: tags });
  },
  getCreatorTags: async () => {
    const tags = await tagsApi.getCreatorTags();
    useTagsStore.setState({ creatorTags: tags });
  },
  updateFavoriteTags: async (tags) => {
    const updatedTags = await tagsApi.updateFavoriteTags(tags);
    useTagsStore.setState({ favoriteTags: updatedTags });
  },
  updateCreatorTags: async (tags) => {
    const updatedTags = await tagsApi.updateCreatorTags(tags);
    useTagsStore.setState({ creatorTags: updatedTags });
  },
});

const useFavoriteTags = () => useTagsStore((store) => store.favoriteTags);
const useCreatorTags = () => useTagsStore((store) => store.creatorTags);

export { tagsActions, useCreatorTags, useFavoriteTags };
