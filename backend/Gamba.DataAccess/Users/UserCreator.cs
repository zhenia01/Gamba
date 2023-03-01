using Gamba.DataAccess.BuildingBlocks;
using Gamba.DataAccess.Users.Rules;

namespace Gamba.DataAccess.Users;

public class UserCreator : Entity
{
    private UserId _followerId;
    
    public User Creator { get; }
    
    public DateTime FollowedAt { get; }

    private UserCreator() {} // EF

    private UserCreator(UserId followerId, User creator)
    {
        _followerId = followerId;
        Creator = creator;
        FollowedAt = DateTime.UtcNow;
    }

    internal static UserCreator AddFollower(User follower, User creator)
    {
        CheckRule(new UserMustBeCreatorRule(creator.IsCreator));
        CheckRule(new CreatorCantFollowHimselfRule(follower.Id, creator.Id));

        return new UserCreator(follower.Id, creator);
    }
}