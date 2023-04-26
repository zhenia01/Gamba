using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.Tags.UpdateTags.AddFavoriteTags;

public class AddFavoriteTagsCommandHandler : UpdateTagsCommandHandlerBase<AddFavoriteTagsCommand>
{
    public AddFavoriteTagsCommandHandler(
        UserRepository userRepository
    ) : base(userRepository)
    {
    }

    protected override void TagsAction(User user, IEnumerable<Tag> tags)
    {
        user.AddFavoriteTags(tags);
    }

    protected override List<Tag> ResultTagsSelector(User user)
    {
        return user.FavoriteTags.ToList();
    }
}