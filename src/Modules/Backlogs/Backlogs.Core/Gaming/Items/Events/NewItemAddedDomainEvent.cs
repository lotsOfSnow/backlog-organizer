using BacklogOrganizer.Shared.Core.Domain.DomainEvents;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
public record NewItemAddedDomainEvent(Guid BacklogId, Guid ItemId) : DomainEventBaseRecord;
