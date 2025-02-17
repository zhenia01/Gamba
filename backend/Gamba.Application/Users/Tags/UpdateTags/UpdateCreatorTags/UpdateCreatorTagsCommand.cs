namespace Gamba.Application.Users.Tags.UpdateTags.UpdateCreatorTags;

public record UpdateCreatorTagsCommand(Guid UserId, IEnumerable<string> Tags) : UpdateTagsCommandBase(UserId, Tags);