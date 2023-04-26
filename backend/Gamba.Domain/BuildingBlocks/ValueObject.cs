namespace Gamba.Domain.BuildingBlocks;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != this.GetType()) return false;

        return ((ValueObject)obj).GetAtomicValues().SequenceEqual(this.GetAtomicValues());
    }
    
    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return Equals(one, two);
    }

    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return Equals(one, two);
    }
    
    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject? other)
    {
        return Equals(obj: other);
    }
}