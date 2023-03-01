using FluentAssertions;
using Gamba.DataAccess.BuildingBlocks;
using Gamba.DataAccess.Users;
using Gamba.DataAccess.Users.Rules;
using Gamba.UnitTests.Users.TestHelpers;
using Xunit;

namespace Gamba.UnitTests.Users.Domain;

public class UserFollowingTests
{
    private static User CreateUser() => User.CreateRegistered("name", "password", Mocks.UserUniquenessChecker);
    
    [Fact]
    public void FollowCreator_FollowingNotCreator_ThrowsBusinessException()
    {
        var follower = CreateUser();
        var notCreator = CreateUser();

        var following = () => follower.FollowCreator(notCreator);
        
        notCreator.IsCreator.Should().BeFalse();
        following
            .Should().Throw<BusinessRuleValidationException>()
            .Where(e => e.BrokenRule is UserMustBeCreatorRule);
    }
    
    [Fact]
    public void FollowCreator_FollowingThemselves_ThrowsBusinessException()
    {
        var followerCreator = CreateUser();
        followerCreator.UpgradeToCreator();

        var following = () => followerCreator.FollowCreator(followerCreator);
        
        following
            .Should().Throw<BusinessRuleValidationException>()
            .Where(e => e.BrokenRule is CreatorCantFollowHimselfRule);
    }
    
    [Fact]
    public void FollowCreator_FollowingCreator_CreatesValidUserCreatorRelation()
    {
        var follower = CreateUser();
        
        var creator = CreateUser();
        creator.UpgradeToCreator();

        follower.FollowCreator(creator);

        follower.FollowingCreators
            .Should().Satisfy(u => u == creator);
    }
}