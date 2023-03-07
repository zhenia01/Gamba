using FluentAssertions;
using Gamba.Domain.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.UnitTests.Users.Infrastructure.TestHelpers;
using Gamba.UnitTests.Users.TestHelpers;
using Xunit;
using Xunit.Abstractions;

namespace Gamba.UnitTests.Users.Infrastructure;

public class UserFollowingTests
{
    [Fact]
    public async Task FollowCreator_FollowingCreator_CreatesValidUserCreatorRelation()
    {
        await using var context = TestGambaContextFactory.CreateContext();
        var repository = new UserRepository(context);
        var follower = User.CreateRegistered("follower", "password", Mocks.UserUniquenessChecker);
        var creator = User.CreateRegistered("creator", "password", Mocks.UserUniquenessChecker);
        creator.UpgradeToCreator();
        
        follower.FollowCreator(creator);
        await repository.Add(follower);
        await repository.Add(creator);
        await repository.SaveChanges();

        var savedFollower = await repository.GetById(follower.Id);
        savedFollower.FollowingCreators
            .Should().Satisfy(c => c.Id == creator.Id);
    }
}