namespace Gamba.DataAccess.BuildingBlocks;

public interface IBusinessRule
{
    bool IsBroken { get; }
    string Message { get; }
}