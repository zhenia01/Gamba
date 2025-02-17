using System.Security.Claims;
using Gamba.Application.Users.Tags.GetTags.GetCreatorTags;
using Gamba.Application.Users.Tags.UpdateTags.UpdateCreatorTags;
using Gamba.Domain.Users.Tags;
using Gamba.WebAPI.Features.Tags.UpdateTags;
using Gamba.WebAPI.Filters;
using Gamba.WebAPI.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gamba.WebAPI.Features.Tags.GetTags;

public static class GetCreatorTagsRequest
{
    public static async Task<Ok<List<Tag>>> Request(ClaimsPrincipal user, IMediator dispatcher)
    {
        var userId = user.GetIdFromClaim();
        var tags = await dispatcher.Send(new GetCreatorTagsQuery(userId));
        return TypedResults.Ok(tags);
    }
}