using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users.Common;

namespace Gamba.Application.Users.RegisterUser;

public record RegisterUserCommand(string Name, string Password): ICommand<UserDto>;