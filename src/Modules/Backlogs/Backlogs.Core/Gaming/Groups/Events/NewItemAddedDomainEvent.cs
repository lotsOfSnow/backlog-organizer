using BacklogOrganizer.Shared.Core.Domain;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;

public class NewItemAddedDomainEvent : DomainEventBase
{
    public NewItemAddedDomainEvent(Guid groupId, Guid itemId)
    {
        GroupId = groupId;
        ItemId = itemId;
    }

    public Guid GroupId { get; }

    public Guid ItemId { get; }
}
