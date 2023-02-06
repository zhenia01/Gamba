using System.IdentityModel.Tokens.Jwt;
using Gamba.Application.Users.Common;
using Gamba.Application.Users.DomainServices;
using Gamba.Application.Users.GetUserByCredentials;
using Gamba.Application.Users.GetUserById;
using Gamba.Application.Users.RegisterUser;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.WebAPI.Features.SeedWork;
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

    private record AuthRequest(string Name, string Password);
    private record AuthResponse(UserDto User, string Token);

    private static async Task<Created<AuthResponse>> SignUp(AuthRequest authRequest, IMediator dispatcher)
    {
        var (name, password) = authRequest;
        var (user, token) = await dispatcher.Send(new RegisterUserCommand(name, password));
        return TypedResults.Created($"/users/{user.Id}", new AuthResponse(user, token));
    }

    private static async Task<Ok<UserDto>> GetCurrentUser(HttpContext httpContext, IMediator dispatcher)
    {
        var userId = Guid.Parse(httpContext.User.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
        var user = await dispatcher.Send(new GetUserByIdQuery(userId));
        return TypedResults.Ok(user);
    }
    
    private static async Task<Ok<AuthResponse>> SignIn(AuthRequest authRequest, IMediator dispatcher)
    {
        var (name, password) = authRequest;
        var (user, token) = await dispatcher.Send(new GetUserByCredentialsQuery(name, password));
        return TypedResults.Ok(new AuthResponse(user, token));
    }
}