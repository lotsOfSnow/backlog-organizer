using BacklogOrganizer.Shared.Core.Domain.DomainEvents;

namespace BacklogOrganizer.Shared.Core.Domain.Entities;

public class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
}
