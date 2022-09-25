using MediatR;

namespace BacklogOrganizer.Shared.Core.Domain.DomainEvents;
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
