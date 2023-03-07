using Gamba.Domain.Users;
using Moq;

namespace Gamba.UnitTests.Users.TestHelpers;

public static class Mocks
{
    public static readonly IUserUniquenessChecker UserUniquenessChecker =
        Mock.Of<IUserUniquenessChecker>(ch => ch.IsUnique(It.IsAny<string>()) == true);
}