import { HStack, Tag as TagElement, TagLabel } from '@chakra-ui/react';

import { Tag } from '@/features/tags';

function TagsList({ tags }: { tags: Tag[] }) {
  return (
    <HStack spacing={4}>
      {tags.map((tag) => (
        <TagElement key={tag.name}>
          <TagLabel>{tag.name}</TagLabel>
        </TagElement>
      ))}
    </HStack>
  );
}

export { TagsList };
