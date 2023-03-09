namespace Gamba.Domain.BuildingBlocks;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}