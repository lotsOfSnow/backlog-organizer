using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Models;

public class Backlog<TItem> : EntityBase, IAggregateRoot
    where TItem : BacklogItem
{
}
