using Gamba.Application.Configuration.Queries;
using Gamba.Application.Users.Common;

namespace Gamba.Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id): IQuery<UserDto>;