using BacklogOrganizer.Shared.Core.Domain.DomainEvents;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;

public record NewPlatformAssignedDomainEvent(Guid ItemId, Guid PlatformId) : DomainEventBaseRecord;
