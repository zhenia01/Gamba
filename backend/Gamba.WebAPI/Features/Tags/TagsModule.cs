using Gamba.Domain.Users;
using Gamba.WebAPI.Features.SeedWork;
using Gamba.WebAPI.Features.Tags.GetTags;
using Gamba.WebAPI.Features.Tags.UpdateTags;

namespace Gamba.WebAPI.Features.Tags;

public class TagsModule: IFeatureModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        const string basePath = "user/tags";
        
        var favoriteTagsGroup = endpoints
            .MapGroup($"{basePath}/favorite")
            .RequireAuthorization();
        favoriteTagsGroup.MapPut("/", UpdateFavoriteTagsRequest.Request);
        favoriteTagsGroup.MapGet("/", GetFavoriteTagsRequest.Request);
        
        var creatorTagsGroup = endpoints
            .MapGroup($"{basePath}/creator")
            .RequireAuthorization(p => p.RequireRole(UserRoles.Creator));
        creatorTagsGroup.MapPut("/", UpdateCreatorTagsRequest.Request);
        creatorTagsGroup.MapGet("/", GetCreatorTagsRequest.Request);


        return endpoints;
    }
}