using BacklogOrganizer.Shared.Core.Domain;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;
public class NewGroupAddedDomainEvent : DomainEventBase
{
    public NewGroupAddedDomainEvent(Guid backlogId, Guid groupId)
    {
        BacklogId = backlogId;
        GroupId = groupId;
    }

    public Guid BacklogId { get; }

    public Guid GroupId { get; }
}
