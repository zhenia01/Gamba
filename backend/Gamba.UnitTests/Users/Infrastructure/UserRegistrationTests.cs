using FluentAssertions;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Domain.Users;
using Gamba.UnitTests.Users.Infrastructure.TestHelpers;
using Moq;
using Xunit;
using Mocks = Gamba.UnitTests.Users.TestHelpers.Mocks;

namespace Gamba.UnitTests.Users.Infrastructure;

public class UserRegistrationTests
{
    [Fact]
    public async Task CreateRegistered_User_CreatesValidUser()
    {
        await using var context = TestGambaContextFactory.CreateContext();
        var repository = new UserRepository(context);
        var user = User.CreateRegistered("name", "password", Mocks.UserUniquenessChecker);

        await repository.Add(user);
        await repository.SaveChanges();

        context.Users.Count().Should().Be(1);
        var savedUser = await repository.GetById(user.Id);
        savedUser.Name.Should().Be("name");
        savedUser.IsCreator.Should().Be(false);
    }
}