using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;

public class GameBacklogItemsGroup : EntityBase
{
    private readonly HashSet<GameBacklogItem> _items = new();

    public Guid BacklogId { get; private set; }

    public GameBacklogItemsGroup(Guid id, Guid backlogId, string name)
    {
        Id = id;
        BacklogId = backlogId;
        Name = name;

        AddDomainEvent(new NewGroupAddedDomainEvent(BacklogId, Name));
    }

    public string Name { get; private set; }

    public void AddItems(params GameBacklogItem[] items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(GameBacklogItem item)
    {
        if (_items.Add(item))
        {
            AddDomainEvent(new NewItemAddedDomainEvent(Id, item.Id));
        }
    }
}
