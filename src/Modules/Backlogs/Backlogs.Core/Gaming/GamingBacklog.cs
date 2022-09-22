using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Models;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public class GamingBacklog : Backlog<GameBacklogItem>
{
    public static readonly Guid InstanceId = new("6c24c264-c53d-4f44-adc4-26560e790a73");

    private readonly ICollection<GameBacklogItemsGroup> _groups = new List<GameBacklogItemsGroup>();

    public IEnumerable<GameBacklogItemsGroup> Groups
        => _groups;

    public void AddGroup(GameBacklogItemsGroup group)
    {
        if (Groups.Any(x => x.Name == group.Name || x.Id == group.Id))
        {
            throw new GroupAlreadyExistsException(group.Name);
        }

        _groups.Add(group);
    }

    public void AddItemsToGroup(Guid groupId, IEnumerable<Guid> itemIds)
    {
        var group = Groups.SingleOrDefault(x => x.Id == groupId);

        if (group is null)
        {
            throw new GroupNotFoundException(groupId);
        }

        var itemsToAdd = Items.Where(x => itemIds.Contains(x.Id)).ToArray();

        group.AddItems(itemsToAdd);
    }
}
