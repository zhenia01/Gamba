using Gamba.DataAccess.BuildingBlocks;
using Gamba.DataAccess.Users.Rules;

namespace Gamba.DataAccess.Users;

public class User : Entity, IAggregateRoot
{
    public UserId Id { get; }
    
    public string Name { get; private set; }

    public bool IsCreator { get; private set; }
        
    private string _password;

    private readonly List<UserCreator> _followingCreators;

    public IEnumerable<User> FollowingCreators => _followingCreators.Select(fc => fc.Creator);

    private User() {} // EF

    private User(string name, string password)
    {
        Id = new (Guid.NewGuid());
        Name = name;
        _password = password;
        _followingCreators = new();
    }

    public static User CreateRegistered(string name, string password, IUserUniquenessChecker userUniquenessChecker)
    {
        CheckRule(new UserNameMustBeUniqueRule(userUniquenessChecker, name));
        
        return new User(name, password);
    }

    public void UpgradeToCreator()
    {
        CheckRule(new UserCanBeUpgradedToCreatorOnlyOnceRule(IsCreator));

        IsCreator = true;
    }

    public void FollowCreator(User creator)
    {
        _followingCreators.Add(UserCreator.AddFollower(this, creator));
    }
}