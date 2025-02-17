using System.Security.Claims;
using Gamba.Application.Users.Tags.GetTags.GetFavoriteTags;
using Gamba.Domain.Users.Tags;
using Gamba.WebAPI.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gamba.WebAPI.Features.Tags.GetTags;

public static class GetFavoriteTagsRequest
{
    public static async Task<Ok<List<Tag>>> Request(ClaimsPrincipal user, IMediator dispatcher)
    {
        var userId = user.GetIdFromClaim();
        var tags = await dispatcher.Send(new GetFavoriteTagsQuery(userId));
        return TypedResults.Ok(tags);
    }
}