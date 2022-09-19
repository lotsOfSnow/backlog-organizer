using BacklogOrganizer.Shared.Core.Extensions;
using BacklogOrganizer.Shared.Core.Results.Errors;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;
public static class GamingBacklogResultErrors
{
    public static ResultError GetBacklogNotFoundError(Guid id)
        => new(ErrorReason.ResourceNotFound, $"Backlog with Id \"{id}\" doesn't exist");

    public static ResultError GetGroupNotFoundError(Guid backlogId, Guid groupId)
        => new(ErrorReason.ResourceNotFound, $"Group with Id {groupId.InQuotationMarks()} doesn't exist within Backlog with Id {backlogId.InQuotationMarks()}");
}
