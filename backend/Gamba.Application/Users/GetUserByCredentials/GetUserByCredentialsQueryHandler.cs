using BCrypt.Net;
using Gamba.Application.Configuration.Queries;
using Gamba.Application.Users.Common;
using Gamba.Infrastructure.Domain.Users;
using Gamba.Infrastructure.Services;

namespace Gamba.Application.Users.GetUserByCredentials;

public class GetUserByCredentialsQueryHandler : IQueryHandler<GetUserByCredentialsQuery, (UserDto user, string token)>
{
    private readonly UserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;

    public GetUserByCredentialsQueryHandler(
        UserRepository userRepository,
        JwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<(UserDto user, string token)> Handle(GetUserByCredentialsQuery query, CancellationToken cancellationToken)
    {
        var (name, password) = query;

        var user = await _userRepository.GetByName(name);
        if (!user.VerifyPassword(hashedPassword => PasswordService.CheckPassword(password, hashedPassword)))
        {
            throw new BcryptAuthenticationException("Wrong password provided");
        }
        
        string token = _jwtTokenService.GenerateToken(user.Id, user.Name);

        return (new(user.Id, user.Name, user.IsCreator), token);
    }
}