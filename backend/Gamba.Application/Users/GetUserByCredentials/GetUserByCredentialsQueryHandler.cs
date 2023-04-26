using System.Security.Authentication;
using Gamba.Application.Configuration.Queries;
using Gamba.Application.Exceptions;
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

        var user = await _userRepository.GetByName(name) ?? throw new EntityNotFoundException($"User {name} does not exist");
        
        if (!user.VerifyPassword(hashedPassword => PasswordService.CheckPassword(password, hashedPassword)))
        {
            throw new AuthenticationException("Wrong password provided");
        }
        
        string token = _jwtTokenService.GenerateToken(user);

        return (new UserDto(user), token);
    }
}