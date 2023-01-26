using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users;
using Gamba.Application.Users.DomainServices;
using Gamba.Application.Users.RegisterUser;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.WebAPI.Features.SeedWork;
using MediatR;

namespace Gamba.WebAPI.Features;

public class UserModule: IFeatureModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddTransient<IUserUniquenessChecker, UserUniquenessChecker>();
        services.AddTransient<UserRepository>();
        
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("users")
            .MapPost("/", RegisterUser);
        
        return endpoints;
    }

    private static async Task<IResult> RegisterUser(string name, string password,
        IMediator dispatcher)
    {
        var id = await dispatcher.Send(new RegisterUserCommand(name, password));
        return TypedResults.Created($"/users/{id}");
    }
}