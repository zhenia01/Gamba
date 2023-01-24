﻿namespace Gamba.DataAccess.BuildingBlocks;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}