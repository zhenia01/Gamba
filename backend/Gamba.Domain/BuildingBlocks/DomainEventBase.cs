namespace Gamba.Domain.BuildingBlocks;

public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}