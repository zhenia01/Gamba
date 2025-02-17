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

        var addingCreatorTags = () => notCreator.UpdateCreatorTags(new Tag[]{new ("tag")});

        addingCreatorTags
            .Should().Throw<BusinessRuleValidationException>()
            .Where(e => e.BrokenRule is UserMustBeCreatorRule);
    }
    
    [Fact]
    public void ChangingTags_ForUserWithExistingTags_ChangesTags()
    {
        var user = User.CreateRegistered("name123", "password", Mocks.UserUniquenessChecker);
        var favoriteTags = new Tag[] { new("tag"), new("cat"), new("dog") };
        var creatorTags = new Tag[] { new("tag"), new("cat"), new("dog") };
        user.UpgradeToCreator();
        user.UpdateFavoriteTags(favoriteTags);
        user.UpdateCreatorTags(creatorTags);

        void UpdatingFavoriteTags() => user.UpdateFavoriteTags(favoriteTags.Concat(new Tag[]{ new("new"), new("new1") }));
        void UpdatingCreatorTags() => user.UpdateCreatorTags(favoriteTags.Concat(new Tag[] { new("new"), new("new1") }));

        user.FavoriteTags.Should().HaveCount(3);
        user.CreatorTags.Should().HaveCount(3);
        UpdatingCreatorTags();
        UpdatingFavoriteTags();
        user.FavoriteTags.Should().HaveCount(5);
        user.CreatorTags.Should().HaveCount(5);
    }
}