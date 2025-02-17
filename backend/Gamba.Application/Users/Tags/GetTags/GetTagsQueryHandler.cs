using Gamba.Application.Configuration.Queries;
using Gamba.Application.Exceptions;
using Gamba.Application.Users.Tags.GetTags.GetCreatorTags;
using Gamba.Application.Users.Tags.GetTags.GetFavoriteTags;
using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.Tags.GetTags;

public class GetTagsQueryHandler
    : IQueryHandler<GetFavoriteTagsQuery, List<Tag>>,
        IQueryHandler<GetCreatorTagsQuery, List<Tag>>
{
    private readonly UserRepository _userRepository;

    public GetTagsQueryHandler(
        UserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    private async Task<List<Tag>> HandleBase(GetTagsQueryBase request, CancellationToken _)
    {
        var userId = request.UserId;
        var user = await _userRepository.GetById(new UserId(userId)) ??
                   throw new EntityNotFoundException("User not found");

        return request.ResultTagsSelector(user);
    }

    public Task<List<Tag>> Handle(GetFavoriteTagsQuery request, CancellationToken cancellationToken)
    {
        return HandleBase(request, cancellationToken);
    }

    public Task<List<Tag>> Handle(GetCreatorTagsQuery request, CancellationToken cancellationToken)
    {
        return HandleBase(request, cancellationToken);
    }
}