using BacklogOrganizer.Shared.Core.Domain;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
public record NewItemAddedDomainEvent(Guid BacklogId, Guid ItemId) : DomainEventBaseRecord;
