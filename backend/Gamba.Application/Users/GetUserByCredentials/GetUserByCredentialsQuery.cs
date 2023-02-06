using Gamba.Application.Configuration.Queries;
using Gamba.Application.Users.Common;

namespace Gamba.Application.Users.GetUserByCredentials;

public record GetUserByCredentialsQuery(string Name, string Password): IQuery<(UserDto user, string token)>;