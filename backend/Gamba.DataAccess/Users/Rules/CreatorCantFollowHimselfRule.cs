using Gamba.DataAccess.BuildingBlocks;

namespace Gamba.DataAccess.Users.Rules;

public class CreatorCantFollowHimselfRule : IBusinessRule
{
    public CreatorCantFollowHimselfRule(UserId followerId, UserId creatorId)
    {
        IsBroken = followerId == creatorId;
    }
    
    public bool IsBroken { get; }
    public string Message => "Creator can't follow himself";
}