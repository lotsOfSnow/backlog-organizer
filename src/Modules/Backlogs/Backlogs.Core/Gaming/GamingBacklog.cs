using BacklogOrganizer.Modules.Backlogs.Core.Models;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public class GamingBacklog : Backlog<GameBacklogItem>
{
    public static readonly Guid InstanceId = new("6c24c264-c53d-4f44-adc4-26560e790a73");
}
