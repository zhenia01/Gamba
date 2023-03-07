using Gamba.Application.Configuration.Queries;
using Gamba.Application.Exceptions;
using Gamba.Application.Users.Common;
using Gamba.Domain.Users;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.GetUserById;

public class GetUserByIdQueryHandler: IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly UserRepository _userRepository;

    public GetUserByIdQueryHandler(
        UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var id = query.Id;

        var user = await _userRepository.GetById(new UserId(id)) ?? throw new EntityNotFoundException();

        return new(user.Id, user.Name, user.IsCreator);
    }
}