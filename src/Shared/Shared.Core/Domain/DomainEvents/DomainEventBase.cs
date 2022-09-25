namespace BacklogOrganizer.Shared.Core.Domain.DomainEvents;
public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }

    public DomainEventBase() => Id = Guid.NewGuid();
}
