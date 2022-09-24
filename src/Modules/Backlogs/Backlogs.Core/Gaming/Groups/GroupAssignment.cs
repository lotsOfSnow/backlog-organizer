using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
public record GroupAssignment(Guid GroupId, Guid ItemId)
{
    public GameBacklogItemsGroup Group { get; set; } = null!;

    public GameBacklogItem Item { get; set; } = null!;
}
