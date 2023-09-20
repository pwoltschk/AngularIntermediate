using Domain.Primitives;

namespace Domain.Events;
public sealed record WorkItemAssignedDomainEvent(int id) : IDomainEvent
{
}

