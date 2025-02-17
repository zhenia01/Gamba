using Dawn;
using Gamba.Domain.BuildingBlocks;

namespace Gamba.Domain.Users.Tags;

public class Tag : ValueObject
{
    public string Name { get; }
    
    private Tag() {} // EF

    public Tag(string name)
    {
        name = name.Trim();

        MyGuard.Argument(name).MinLength(3).MaxLength(15).Require(n => n.All(char.IsLetterOrDigit), _ => "Tag should only contain alphanumeric characters");

        Name = name;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name.ToLower();
    }
}