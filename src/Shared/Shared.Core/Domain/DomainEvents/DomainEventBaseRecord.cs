namespace BacklogOrganizer.Shared.Core.Domain.DomainEvents;

// TODO: Probably fine to drop the class DomainEventBase and use this only.
public record DomainEventBaseRecord : IDomainEvent
{
    public Guid Id { get; }

    public DomainEventBaseRecord()
        => Id = Guid.NewGuid();
}
