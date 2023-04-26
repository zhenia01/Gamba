using System.IdentityModel.Tokens.Jwt;
using Gamba.Application.Users.Tags.UpdateTags.AddFavoriteTags;
using Gamba.Domain.Users.Tags;
using Gamba.WebAPI.Features.SeedWork;
using Gamba.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gamba.WebAPI.Features.Tags;

public class TagsModule: IFeatureModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var favoriteTagsGroup = endpoints
            .MapGroup("user/tags/favorite");
        favoriteTagsGroup.MapPatch("/", AddFavoriteTags)
            .RequireAuthorization();
        
        // var creatorTagsGroup = endpoints
        //     .MapGroup("user/tags/creator")
        //     .RequireAuthorization(p => p.RequireRole("Creator"));

        return endpoints;
    }
    
    private static async Task<Ok<List<Tag>>> AddFavoriteTags([Validate]AddFavoriteTagsCommand requestBody, HttpContext httpContext, IMediator dispatcher)
    {
        var userId = Guid.Parse(httpContext.User.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
        requestBody = requestBody with { Id = userId };
        var tags = await dispatcher.Send(requestBody);
        return TypedResults.Ok(tags);
    }
}