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
        if (_groups.Any(x => x.Name == group.Name))
        {
            throw new GroupAlreadyExistsException();
        }

        _groups.Add(group);
    }
}
