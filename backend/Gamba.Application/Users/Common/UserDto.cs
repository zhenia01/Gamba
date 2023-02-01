using Gamba.DataAccess.Users;

namespace Gamba.Application.Users.Common;

public record UserDto(Guid Id, string Name, bool IsCreator);