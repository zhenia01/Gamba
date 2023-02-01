using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users;
using Gamba.Application.Users.Common;
using Gamba.Application.Users.DomainServices;
using Gamba.Application.Users.RegisterUser;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.WebAPI.Features.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gamba.WebAPI.Features;

public class AuthModule: IFeatureModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddTransient<IUserUniquenessChecker, UserUniquenessChecker>();
        services.AddTransient<UserRepository>();
        
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("auth");

        group.MapPost("/sign-up", SignUp);
        group.MapPost("/sign-in", SignIn);
        group.MapGet("/current-user", GetCurrentUser)
            .RequireAuthorization();

        return endpoints;
    }

    private record AuthRequest(string Name, string Password);
    private record AuthResponse(UserDto User, string Token);

    private static async Task<IResult> SignUp(AuthRequest authRequest,
        IMediator dispatcher)
    {
        var (name, password) = authRequest;
        var user = await dispatcher.Send(new RegisterUserCommand(name, password));
        return TypedResults.Created($"/users/{user.Id}", new AuthResponse(user, "TOKEN"));
    }

    private static async Task<IRequest> GetCurrentUser(IMediator dispatcher)
    {
        throw new NotImplementedException();
    }
    
    private static async Task<IResult> SignIn(AuthRequest authRequest,
        IMediator dispatcher)
    {
        throw new NotImplementedException();
        // var (name, password) = authRequest;
        // var id = await dispatcher.Send(new RegisterUserCommand(name, password));
        // return TypedResults.Created($"/users/{id}");
    }
}