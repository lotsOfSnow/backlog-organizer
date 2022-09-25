using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;

public record AddBacklogItemCommand(Guid BacklogId, string Name) : IRequest<Result<BacklogItemDto>>;
