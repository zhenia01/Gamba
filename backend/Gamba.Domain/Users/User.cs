using Dawn;
using Gamba.Domain.BuildingBlocks;
using Gamba.Domain.Users.Rules;
using Gamba.Domain.Users.Tags;

namespace Gamba.Domain.Users;

public class User : Entity, IAggregateRoot
{
    public UserId Id { get; }
    
    public string Name { get; private set; }

    public bool IsCreator { get; private set; }

    private string _password;

    private readonly List<UserCreator> _followingCreators;

    public List<User> FollowingCreators => _followingCreators.Select(fc => fc.Creator).ToList();

    private List<Tag>? _creatorTags;

    public List<Tag>? CreatorTags => _creatorTags?.AsReadOnly().ToList();
    
    private readonly List<Tag> _favoriteTags;

    public List<Tag> FavoriteTags => _favoriteTags.AsReadOnly().ToList();

    private User() {} // EF

    private User(string name, string password)
    {
        Id = new (Guid.NewGuid());
        Name = MyGuard.Argument(name).MinLength(5).MaxLength(20);
        _password = MyGuard.Argument(password).MinLength(5).MaxLength(20);
        _followingCreators = new();
        _favoriteTags = new();
    }

    public static User CreateRegistered(string name, string password, IUserUniquenessChecker userUniquenessChecker)
    {
        CheckRule(new UserNameMustBeUniqueRule(userUniquenessChecker, name));
        
        return new User(name, password);
    }

    public void UpgradeToCreator()
    {
        CheckRule(new UserCanBeUpgradedToCreatorOnlyOnceRule(IsCreator));

        _creatorTags = new();
        IsCreator = true;
    }

    public void FollowCreator(User creator)
    {
        _followingCreators.Add(UserCreator.AddFollower(this, creator));
    }
    
    public void UpdateFavoriteTags(IEnumerable<Tag> tags)
    {
        _favoriteTags.Clear();
        _favoriteTags.AddRange(tags);
    }
    
    public void UpdateCreatorTags(IEnumerable<Tag> tags)
    {
        CheckRule(new UserMustBeCreatorRule(IsCreator));
        
        _creatorTags!.Clear();
        _creatorTags!.AddRange(tags);
    }
    
    public bool VerifyPassword(Predicate<string> checker) => checker(_password);
}