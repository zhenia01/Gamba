using Gamba.Domain.BuildingBlocks;

namespace Gamba.Domain.Users;

public record UserId : IdValueBase
{
    public UserId(Guid value) : base(value)
    {
    }
}