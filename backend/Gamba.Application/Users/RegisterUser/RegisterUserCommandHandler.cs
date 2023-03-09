using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users.Common;
using Gamba.Domain.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.Infrastructure.Services;

namespace Gamba.Application.Users.RegisterUser;

public class RegisterUserCommandHandler: ICommandHandler<RegisterUserCommand, (UserDto user, string token)>
{
    private readonly UserRepository _userRepository;
    private readonly IUserUniquenessChecker _userUniquenessChecker;
    private readonly JwtTokenService _jwtTokenService;

    public RegisterUserCommandHandler(
        UserRepository userRepository,
        IUserUniquenessChecker userUniquenessChecker,
        JwtTokenService jwtTokenService
        )
    {
        _userRepository = userRepository;
        _userUniquenessChecker = userUniquenessChecker;
        _jwtTokenService = jwtTokenService;
    }
    
    public async Task<(UserDto user, string token)> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var (name, password) = command;
        password = PasswordService.HashPassword(password);
        
        var user = User.CreateRegistered(name, password, _userUniquenessChecker);
        
        await _userRepository.Add(user);
        await _userRepository.SaveChanges();

        string token = _jwtTokenService.GenerateToken(user.Id, user.Name);
        
        return (new(user.Id, user.Name, user.IsCreator), token);
    }
}