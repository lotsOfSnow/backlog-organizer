using BacklogOrganizer.Modules.Backlogs.Core.Models;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.ChangeStatus;

public record ChangeStatusEndpointRequest(Guid BacklogId, ItemCompletionStatus NewStatus);
