using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;

namespace Gamba.Application.Users.Common;

public record UserDto(Guid Id, string Name, bool IsCreator)
{
    public UserDto(User user): this(user.Id, user.Name, user.IsCreator)
    {
    }
}