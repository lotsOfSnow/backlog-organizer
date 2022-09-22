using BacklogOrganizer.Shared.Core.Exceptions;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
public class GroupNotFoundException : CustomException
{
    public Guid GroupId { get; }

    public GroupNotFoundException(Guid groupId)
    {
        GroupId = groupId;
    }
}
