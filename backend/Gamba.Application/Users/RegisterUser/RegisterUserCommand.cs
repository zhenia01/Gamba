using Gamba.Application.Configuration.Commands;

namespace Gamba.Application.Users.RegisterUser;

public record RegisterUserCommand(string Name, string Password): ICommand<Guid>;