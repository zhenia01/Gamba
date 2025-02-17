using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.Tags.UpdateTags.UpdateCreatorTags;

public class UpdateCreatorTagsCommandHandler : UpdateTagsCommandHandlerBase<UpdateCreatorTagsCommand>
{
    public UpdateCreatorTagsCommandHandler(
        UserRepository userRepository
    ) : base(userRepository)
    {
    }

    protected override void TagsAction(User user, IEnumerable<Tag> tags)
    {
        user.UpdateCreatorTags(tags);
    }

    protected override List<Tag> ResultTagsSelector(User user)
    {
        return user.CreatorTags!.ToList();
    }
}