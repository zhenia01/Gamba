using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.Tags.UpdateTags.UpdateFavoriteTags;

public class UpdateFavoriteTagsCommandHandler : UpdateTagsCommandHandlerBase<UpdateFavoriteTagsCommand>
{
    public UpdateFavoriteTagsCommandHandler(
        UserRepository userRepository
    ) : base(userRepository)
    {
    }

    protected override void TagsAction(User user, IEnumerable<Tag> tags)
    {
        user.UpdateFavoriteTags(tags);
    }

    protected override List<Tag> ResultTagsSelector(User user)
    {
        return user.FavoriteTags.ToList();
    }
}