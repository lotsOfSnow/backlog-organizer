namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming.Features.AddItem;

public record AddItemEndpointRequestContract(string Name)
{
    public AddBacklogItemCommand CreateCommand(Guid backlogId)
        => new(backlogId, Name);
}
