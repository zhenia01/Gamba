using FluentAssertions;
using Gamba.Domain.Users;
using Gamba.UnitTests.Users.TestHelpers;
using Xunit;

namespace Gamba.UnitTests.Users.Domain;

public class UserRegistrationTests
{
    [Fact]
    public void CreateRegistered_User_CreatesValidUser()
    {
        var user = User.CreateRegistered("name", "password", Mocks.UserUniquenessChecker);

        user.Name.Should().Be("name");
        user.IsCreator.Should().Be(false);
    }
}