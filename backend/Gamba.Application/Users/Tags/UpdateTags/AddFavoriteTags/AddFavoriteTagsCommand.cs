namespace Gamba.Application.Users.Tags.UpdateTags.AddFavoriteTags;

public record AddFavoriteTagsCommand(Guid Id, IEnumerable<string> Tags) : UpdateTagsCommandBase(Id, Tags);