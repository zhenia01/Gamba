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

    public void AddCreatorTags(IEnumerable<Tag> tags)
    {
        CheckRule(new UserMustBeCreatorRule(IsCreator));

        _creatorTags!.AddRange(tags.Where(t => !_creatorTags.Contains(t)));
    }
    
    public void AddFavoriteTags(IEnumerable<Tag> tags)
    {
        _favoriteTags.AddRange(tags.Where(t => !_favoriteTags.Contains(t)));
    }
    
    public void RemoveCreatorTags(IEnumerable<Tag> tags)
    {
        CheckRule(new UserMustBeCreatorRule(IsCreator));
        
        _creatorTags?.RemoveAll(tags.Contains);
    }
    
    public void RemoveFavoriteTags(IEnumerable<Tag> tags)
    {
        _favoriteTags.RemoveAll(tags.Contains);
    }


    public bool VerifyPassword(Predicate<string> checker) => checker(_password);
}