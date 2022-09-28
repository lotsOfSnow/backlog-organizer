using Ardalis.GuardClauses;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
using BacklogOrganizer.Shared.Core;
using BacklogOrganizer.Shared.Core.Domain.Entities;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public class Backlog : GuidIdEntity, IAggregateRoot
{
    public static readonly Guid InstanceId = new("6c24c264-c53d-4f44-adc4-26560e790a73");

    private readonly HashSet<BacklogItem> _items = new();
    private readonly HashSet<BacklogGroup> _groups = new();

    public IEnumerable<BacklogItem> Items => _items.ToList().AsReadOnly();

    public Backlog(Guid id)
        : base(id)
    {
    }

    public void AddItem(BacklogItem item)
    {
        Guard.Against.Null(item, nameof(item));

        if (_items.Add(item))
        {
            AddDomainEvent(new NewItemAddedDomainEvent(Id, item.Id));
        }
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.SingleOrDefault(x => x.Id == itemId);

        if (item is null)
        {
            throw new BacklogItemNotFoundException(Id, itemId);
        }

        _items.Remove(item);
    }

    public void UpdateItemPlatforms(Guid itemId, IEnumerable<Platform> newPlatforms)
    {
        if (newPlatforms.Any(x => !x.IsDefault))
        {
            throw new NotImplementedException("Adding custom platforms isn't supported at the moment");
        }

        var item = _items.SingleOrDefault(x => x.Id == itemId);

        if (item is null)
        {
            throw new BacklogItemNotFoundException(Id, itemId);
        }

        item.UpdatePlatforms(newPlatforms);
    }

    public void AddGroup(BacklogGroup group)
    {
        // TODO: Identify group by its name and its backlog
        if (_groups.Any(x => x.Name == group.Name || x.Id == group.Id))
        {
            throw new GroupAlreadyExistsException(group.Name);
        }

        _groups.Add(group);
    }

    public void AddItemsToGroup(Guid groupId, IEnumerable<Guid> itemIds)
    {
        var group = _groups.SingleOrDefault(x => x.Id == groupId);

        if (group is null)
        {
            throw new GroupNotFoundException(groupId);
        }

        var itemsToAdd = Items.Where(x => itemIds.Contains(x.Id)).ToArray();

        group.AddItems(itemsToAdd);
    }
}
