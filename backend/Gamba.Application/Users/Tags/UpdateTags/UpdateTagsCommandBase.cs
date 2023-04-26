using FluentValidation;
using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users.Tags.UpdateTags.AddFavoriteTags;
using Gamba.Domain.Users.Tags;

namespace Gamba.Application.Users.Tags.UpdateTags;

public record UpdateTagsCommandBase(Guid Id, IEnumerable<string> Tags) : ICommand<List<Tag>>
{
    public class Validator : AbstractValidator<AddFavoriteTagsCommand>
    {
        public Validator()
        {
            RuleForEach(c => c.Tags).Must(tag =>
            {
                try
                {
                    var _ = new Tag(tag);
                    return true;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            });
        }
    }
}