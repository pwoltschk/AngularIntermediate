using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Primitives;

public abstract class AggregateRoot : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected AggregateRoot(int id) : base(id)
    {
    }
    protected AggregateRoot()
    {

    }

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearEvents() => _domainEvents.Clear();

    public void AddEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}