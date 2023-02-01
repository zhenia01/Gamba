using Gamba.Application.Configuration.Commands;
using Gamba.Application.Users.Common;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users.RegisterUser;

public class RegisterUserCommandHandler: ICommandHandler<RegisterUserCommand, UserDto>
{
    private readonly UserRepository _userRepository;
    private readonly IUserUniquenessChecker _userUniquenessChecker;

    public RegisterUserCommandHandler(
        UserRepository userRepository,
        IUserUniquenessChecker userUniquenessChecker)
    {
        _userRepository = userRepository;
        _userUniquenessChecker = userUniquenessChecker;
    }
    
    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var (name, password) = command;
        var user = User.CreateRegistered(name, password, _userUniquenessChecker);
        await _userRepository.Add(user);
        await _userRepository.SaveChanges();
        return new(user.Id, user.Name, user.IsCreator);
    }
}