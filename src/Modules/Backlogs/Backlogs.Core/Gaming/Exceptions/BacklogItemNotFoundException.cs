using BacklogOrganizer.Shared.Core.Exceptions;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
public class BacklogItemNotFoundException : CustomException
{
    public Guid BacklogId { get; }

    public Guid ItemId { get; }

    public BacklogItemNotFoundException(Guid backlogId, Guid itemId)
    {
        BacklogId = backlogId;
        ItemId = itemId;
    }
}
