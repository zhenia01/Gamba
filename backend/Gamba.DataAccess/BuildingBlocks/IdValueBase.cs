namespace Gamba.DataAccess.BuildingBlocks;

public abstract record IdValueBase
{
    public Guid Value { get; }
    
    protected IdValueBase(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        Value = value;
    }
    
    public static implicit operator Guid(IdValueBase id) => id.Value;
}