using Ardalis.GuardClauses;
using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Exceptions;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs;

public class Backlog<TItem> : EntityBase, IAggregateRoot
    where TItem : BacklogItem
{
    private readonly List<TItem> _items = null!;

    public IEnumerable<TItem> Items => _items.AsReadOnly();

    public void AddItem(TItem item)
    {
        Guard.Against.Null(item, nameof(item));

        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);

        if (item is null)
        {
            throw new BacklogItemDoesntExistException(Id, itemId);
        }

        _items.Remove(item);
    }
}
