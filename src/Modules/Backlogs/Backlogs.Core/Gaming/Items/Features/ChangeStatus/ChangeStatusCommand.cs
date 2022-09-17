using BacklogOrganizer.Modules.Backlogs.Core.Models;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.ChangeStatus;

public record ChangeStatusCommand(Guid BacklogId, Guid ItemId, ItemCompletionStatus NewStatus) : IRequest;
