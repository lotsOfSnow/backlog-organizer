using BacklogOrganizer.Shared.Core.Domain;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;
public class NewGroupAddedDomainEvent : DomainEventBase
{
    public NewGroupAddedDomainEvent(Guid backlogId, string groupName)
    {
        BacklogId = backlogId;
        GroupName = groupName;
    }

    public Guid BacklogId { get; }

    public string GroupName { get; }

}
