using Gamba.Application.Users;
using Gamba.Application.Users.DomainServices;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.WebAPI.Features;

public static class UserModule
{
    public static void AddUserModule(this IServiceCollection services)
    {
        services.AddTransient<IUserUniquenessChecker, UserUniquenessChecker>();
        services.AddTransient<UserRepository>();
        services.AddTransient<UserService>();
    }
    
    public static void MapUserModule(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("users")
            .MapPost("/", RegisterUser);
    }

    private static async Task<IResult> RegisterUser(string name, string password, UserService userService)
    {
        var user = await userService.CreateRegisteredUser(name, password);
        return TypedResults.Created($"/users/{user.Id}", user);
    }
}