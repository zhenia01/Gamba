using Gamba.DataAccess.BuildingBlocks;

namespace Gamba.DataAccess.Users.Rules;

public class UserIsCreatorRule : IBusinessRule
{
    public UserIsCreatorRule(bool isCreator)
    {
        IsBroken = !isCreator;
    }
    
    public bool IsBroken { get; }
    public string Message => "User is not a creator.";
}