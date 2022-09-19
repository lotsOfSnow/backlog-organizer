using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;

public class GameBacklogItemsGroup : EntityBase
{
    private readonly HashSet<GameBacklogItem> _items = new();

    public GameBacklogItemsGroup(string name)
        => Name = name;

    public string Name { get; protected set; }

    public IReadOnlyCollection<GameBacklogItem> Items
        => _items;

    public void AddItems(params GameBacklogItem[] items)
    {
        _items.UnionWith(items);
    }
}
