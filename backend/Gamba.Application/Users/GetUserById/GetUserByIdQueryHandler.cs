using Gamba.Application.Configuration.Queries;
using Gamba.Application.Users.Common;
using Gamba.DataAccess.Users;
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

        var user = await _userRepository.GetById(new UserId(id));
        
        return new(user.Id, user.Name, user.IsCreator);
    }
}