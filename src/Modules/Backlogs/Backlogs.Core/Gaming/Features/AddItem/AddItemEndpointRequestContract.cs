namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.AddItem;

public record AddItemEndpointRequestContract(string Name)
{
    public AddBacklogItemCommand CreateCommand(Guid backlogId)
        => new(backlogId, Name);
}
