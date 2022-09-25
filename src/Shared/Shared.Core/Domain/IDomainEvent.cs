using MediatR;

namespace BacklogOrganizer.Shared.Core.Domain;
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
