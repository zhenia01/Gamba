using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;

namespace Gamba.Application.Users.Common;

public record UserDto(Guid Id, string Name, bool IsCreator, IEnumerable<string> FavoriteTags)
{
    public UserDto(User user): this(user.Id, user.Name, user.IsCreator, user.FavoriteTags.Select(t => t.Name))
    {
    }
}