using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;

namespace Gamba.Application.Users.Tags.GetTags.GetCreatorTags;

public record GetCreatorTagsQuery(Guid UserId) : GetTagsQueryBase(UserId)
{
    public override List<Tag> ResultTagsSelector(User user)
    {
        return user.CreatorTags!;
    }
}