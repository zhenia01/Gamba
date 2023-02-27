using FluentValidation;
using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users.Common;

namespace Gamba.Application.Users.RegisterUser;

public record RegisterUserCommand(string Name, string Password) : ICommand<(UserDto user, string token)>
{
    public class Validator : AbstractValidator<RegisterUserCommand>
    {
        public Validator()
        {
            RuleFor(q => q.Name).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(q => q.Password).NotEmpty().MinimumLength(5).MaximumLength(20);
        }
    }
}