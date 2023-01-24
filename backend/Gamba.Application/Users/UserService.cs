using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;

namespace Gamba.Application.Users;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly IUserUniquenessChecker _userUniquenessChecker;

    public UserService(
        UserRepository userRepository,
        IUserUniquenessChecker userUniquenessChecker)
    {
        _userRepository = userRepository;
        _userUniquenessChecker = userUniquenessChecker;
    }

    public async Task<RegisteredUserDto> CreateRegisteredUser(string name, string password)
    {
        var user = User.CreateRegistered(name, password, _userUniquenessChecker);
        await _userRepository.Add(user);
        await _userRepository.SaveChanges();
        return new RegisteredUserDto(user.Id, user.Name);
    }
}