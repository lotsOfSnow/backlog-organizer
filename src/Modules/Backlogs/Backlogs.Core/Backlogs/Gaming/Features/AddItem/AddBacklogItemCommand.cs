using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming.Features.AddItem;

public record AddBacklogItemCommand(Guid BacklogId, string Name) : IRequest
{
    public Guid? AddedItemId { get; set; }
}
