using BacklogOrganizer.Shared.Core.Domain;

namespace BacklogOrganizer.Shared.Core;

public class EntityBase : IEquatable<EntityBase>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public bool Equals(EntityBase? other)
        => other?.Id == Id;

    public override bool Equals(object? obj)
        => Equals(obj as EntityBase);

    public override int GetHashCode()
        => Id.GetHashCode();

}
