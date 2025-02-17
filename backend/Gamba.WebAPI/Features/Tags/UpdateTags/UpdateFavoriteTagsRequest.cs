using System.Security.Claims;
using Gamba.Application.Users.Tags.UpdateTags.UpdateFavoriteTags;
using Gamba.Domain.Users.Tags;
using Gamba.WebAPI.Filters;
using Gamba.WebAPI.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gamba.WebAPI.Features.Tags.UpdateTags;

public static class UpdateFavoriteTagsRequest
{
    public static async Task<Ok<List<Tag>>> Request([Validate]UpdateTagsRequestBody requestBody, ClaimsPrincipal user, IMediator dispatcher)
    {
        var userId = user.GetIdFromClaim();
        var tags = await dispatcher.Send(new UpdateFavoriteTagsCommand(userId, requestBody.Tags));
        return TypedResults.Ok(tags);
    }
}