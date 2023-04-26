using FluentAssertions;
using Gamba.Domain.Users.Tags;
using Xunit;

namespace Gamba.UnitTests.Users.Domain.Tags;

public class CreatingTagTests
{
    [Fact]
    public void CreatingTag_WithSpaces_ThrowsArgumentException()
    {
        var tag = () => new Tag("tag with spaces");
        
        tag
            .Should().Throw<ArgumentException>()
            .Where(e => e.Source == "Dawn.Guard");
    }
    
    [Fact]
    public void CreatingTag_WithSpecialCharacters_ThrowsArgumentException()
    {
        var tag = () => new Tag("tag%%%");
        
        tag
            .Should().Throw<ArgumentException>()
            .Where(e => e.Source == "Dawn.Guard");
    }
    
    [Fact]
    public void CreatedTags_WithExactNames_ShouldBeEqual()
    {
        var tag1 = new Tag("tag");
        var tag2 = new Tag("TAG");
        
        (tag1 == tag2).Should().BeTrue();
        tag1.Equals(tag2).Should().BeTrue();
    }
}