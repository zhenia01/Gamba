using Gamba.Domain.BuildingBlocks;

namespace Gamba.Domain.Users.Rules;

public class UserMustBeCreatorRule : IBusinessRule
{
    public UserMustBeCreatorRule(bool isCreator)
    {
        IsBroken = !isCreator;
    }
    
    public bool IsBroken { get; }
    public string Message => "User is not a creator";
}