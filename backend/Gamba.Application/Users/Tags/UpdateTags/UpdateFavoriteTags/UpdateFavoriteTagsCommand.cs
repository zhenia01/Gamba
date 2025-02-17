namespace Gamba.Application.Users.Tags.UpdateTags.UpdateFavoriteTags;

public record UpdateFavoriteTagsCommand(Guid UserId, IEnumerable<string> Tags) : UpdateTagsCommandBase(UserId, Tags);