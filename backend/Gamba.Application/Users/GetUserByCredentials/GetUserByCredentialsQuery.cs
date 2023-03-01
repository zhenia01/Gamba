using FluentValidation;
using Gamba.Application.Configuration.Queries;
using Gamba.Application.Users.Common;

namespace Gamba.Application.Users.GetUserByCredentials;

public record GetUserByCredentialsQuery(string Name, string Password) : IQuery<(UserDto user, string token)>
{
    public class Validator : AbstractValidator<GetUserByCredentialsQuery>
    {
        public Validator()
        {
            RuleFor(q => q.Name).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(q => q.Password).NotEmpty().MinimumLength(5).MaximumLength(20);
        }
    }
}

