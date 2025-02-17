using FluentValidation;
using Gamba.Application.Configuration.Commands;
using Gamba.Domain.Users.Tags;

namespace Gamba.Application.Users.Tags.UpdateTags;

public record UpdateTagsCommandBase(Guid UserId, IEnumerable<string> Tags) : ICommand<List<Tag>>
{
    public class Validator : AbstractValidator<UpdateTagsCommandBase>
    {
        public Validator()
        {
            RuleForEach(c => c.Tags).Must(tag =>
            {
                try
                {
                    _ = new Tag(tag);
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