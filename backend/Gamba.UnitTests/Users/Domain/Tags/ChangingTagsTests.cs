using FluentAssertions;
using Gamba.Domain.BuildingBlocks;
using Gamba.Domain.Users;
using Gamba.Domain.Users.Rules;
using Gamba.Domain.Users.Tags;
using Gamba.UnitTests.Users.TestHelpers;
using Xunit;

namespace Gamba.UnitTests.Users.Domain.Tags;

public class ChangingTagsTests
{
    [Fact]
    public void ChangingCreatorTags_ForNotCreator_ThrowsBusinessValidationException()
    {
        var notCreator = User.CreateRegistered("name123", "password", Mocks.UserUniquenessChecker);

        var addingCreatorTags = () => notCreator.AddCreatorTags(new Tag[]{new ("tag")});

        addingCreatorTags
            .Should().Throw<BusinessRuleValidationException>()
            .Where(e => e.BrokenRule is UserMustBeCreatorRule);
    }
    
    [Fact]
    public void ChangingTags_WithExactNameForUserWithExistingTag_DoesntChangeTags()
    {
        var user = User.CreateRegistered("name123", "password", Mocks.UserUniquenessChecker);
        user.AddFavoriteTags(new Tag[]{new ("tag"), new ("cat"), new ("dog")});

        var addingExactTag = () => user.AddFavoriteTags(new Tag[]{new ("TAG")});

        user.FavoriteTags.Should().HaveCount(3);
        addingExactTag();
        user.FavoriteTags.Should().HaveCount(3);
    }    
    
    [Fact]
    public void ChangingTags_ForUserWithExistingTags_ChangesTags()
    {
        var user = User.CreateRegistered("name123", "password", Mocks.UserUniquenessChecker);
        user.UpgradeToCreator();
        user.AddFavoriteTags(new Tag[]{new ("tag"), new ("cat"), new ("dog")});
        user.AddCreatorTags(new Tag[]{new ("tag"), new ("cat"), new ("dog")});

        var addingFavoriteTags = () => user.AddFavoriteTags(new Tag[]{new ("new"), new ("new1")});
        var addingCreatorTags = () => user.AddCreatorTags(new Tag[]{new ("new"), new ("new1")});
        var removingFavoriteTags = () => user.RemoveFavoriteTags(new Tag[]{new ("new"), new ("DOG"), new ("cAt")});
        var removingCreatorTags = () => user.RemoveCreatorTags(new Tag[]{new ("new"), new ("DOG"), new ("cAt")});

        user.FavoriteTags.Should().HaveCount(3);
        user.CreatorTags.Should().HaveCount(3);
        addingCreatorTags();
        addingFavoriteTags();
        user.FavoriteTags.Should().HaveCount(5);
        user.CreatorTags.Should().HaveCount(5);
        removingCreatorTags();
        removingFavoriteTags();
        user.FavoriteTags.Should().HaveCount(2);
        user.CreatorTags.Should().HaveCount(2);
    }
}