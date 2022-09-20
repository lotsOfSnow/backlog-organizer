using Ardalis.GuardClauses;
using BacklogOrganizer.Modules.Backlogs.Core.Exceptions;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Models;

public class Backlog<TItem> : EntityBase, IAggregateRoot
    where TItem : BacklogItem
{
    private readonly List<TItem> _items = new();

    public IEnumerable<TItem> Items => _items.AsReadOnly();

    public void AddItem(TItem item)
    {
        Guard.Against.Null(item, nameof(item));

        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.Find(x => x.Id == itemId);

        if (item is null)
        {
            throw new BacklogItemDoesntExistException(Id, itemId);
        }

        _items.Remove(item);
    }
}
