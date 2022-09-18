using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;

public class GameBacklogItemsGroup : EntityBase
{
    public GameBacklogItemsGroup(string name)
        => Name = name;

    public string Name { get; protected set; }

    public ICollection<GameBacklogItem> Items { get; protected set; } = null!;
}
