using FluentValidation;
using Gamba.Application.Users.Tags.UpdateTags;
using Gamba.Domain.Users.Tags;

namespace Gamba.WebAPI.Features.Tags.UpdateTags;

public record UpdateTagsRequestBody(IEnumerable<string> Tags)
{
    public class Validator : UpdateTagsCommandBase.Validator {}
};