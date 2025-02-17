using Gamba.Application.Configuration.Queries;
using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;

namespace Gamba.Application.Users.Tags.GetTags;

public abstract record GetTagsQueryBase(Guid UserId) : IQuery<List<Tag>>
{
    public abstract List<Tag> ResultTagsSelector(User user);
}