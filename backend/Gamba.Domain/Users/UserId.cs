using Gamba.DataAccess.BuildingBlocks;

namespace Gamba.DataAccess.Users;

public record UserId : IdValueBase
{
    public UserId(Guid value) : base(value)
    {
    }
}