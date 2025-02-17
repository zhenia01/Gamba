using System.Security.Claims;
using Gamba.Application.Users.Common;
using Gamba.Application.Users.DomainServices;
using Gamba.Application.Users.GetUserByCredentials;
using Gamba.Application.Users.GetUserById;
using Gamba.Application.Users.RegisterUser;
using Gamba.Domain.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.WebAPI.Features.SeedWork;
using Gamba.WebAPI.Filters;
using Gamba.WebAPI.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gamba.WebAPI.Features.Auth;

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

    private record AuthResponse(UserDto User, string Token);

    private static async Task<Created<AuthResponse>> SignUp([Validate]RegisterUserCommand requestBody, IMediator dispatcher)
    {
        var (user, token) = await dispatcher.Send(requestBody);
        return TypedResults.Created($"/users/{user.Id}", new AuthResponse(user, token));
    }

    private static async Task<Ok<UserDto>> GetCurrentUser(ClaimsPrincipal principal, IMediator dispatcher)
    {
        var userId = principal.GetIdFromClaim();
        var user = await dispatcher.Send(new GetUserByIdQuery(userId));
        return TypedResults.Ok(user);
    }
    
    private static async Task<Ok<AuthResponse>> SignIn([Validate]GetUserByCredentialsQuery requestBody, IMediator dispatcher)
    {
        var (user, token) = await dispatcher.Send(requestBody);
        return TypedResults.Ok(new AuthResponse(user, token));
    }
}