namespace Gamba.Domain.BuildingBlocks;

public interface IBusinessRule
{
    bool IsBroken { get; }
    string Message { get; }
}