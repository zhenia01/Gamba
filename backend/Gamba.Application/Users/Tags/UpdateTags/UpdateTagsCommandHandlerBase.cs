using System.Linq.Expressions;
using Gamba.Application.Configuration.Commands;
using Gamba.Application.Exceptions;
using Gamba.Application.Users.Tags.UpdateTags.AddFavoriteTags;
using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.Tags.UpdateTags;

public abstract class UpdateTagsCommandHandlerBase<TCommand> : ICommandHandler<TCommand, List<Tag>>
    where TCommand: UpdateTagsCommandBase
{
    private readonly UserRepository _userRepository;

    protected UpdateTagsCommandHandlerBase(
        UserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    protected abstract void TagsAction(User user, IEnumerable<Tag> tags);
    protected abstract List<Tag> ResultTagsSelector(User user);

    public async Task<List<Tag>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var (userId, tagsToAdd) = request;
        var user = await _userRepository.GetById(new UserId(userId)) ?? throw new EntityNotFoundException("User's not found");
        
        TagsAction(user, tagsToAdd.Select(t => new Tag(t)));

        await _userRepository.SaveChanges();

        return ResultTagsSelector(user);
    }
}