using Gamba.Domain.BuildingBlocks;

namespace Gamba.Domain.Users.Rules;

public class UserCanBeUpgradedToCreatorOnlyOnceRule : IBusinessRule
{
    public UserCanBeUpgradedToCreatorOnlyOnceRule(bool isCreator)
    {
        IsBroken = isCreator;
    }

    public bool IsBroken { get; }
    public string Message { get; } = "User can be upgraded to Creator only once";
}