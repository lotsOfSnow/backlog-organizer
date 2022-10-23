using BacklogOrganizer.Shared.Core.Extensions;
using BacklogOrganizer.Shared.Core.Results.Errors;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;
public static class BacklogResultErrors
{
    public static ResultError GetBacklogNotFoundError(Guid id)
        => new(ErrorReason.ResourceNotFound, $"Backlog with Id \"{id}\" doesn't exist");

    public static ResultError GetGroupNotFoundError(Guid backlogId, Guid groupId)
        => new(ErrorReason.ResourceNotFound, $"Group with Id {groupId.InQuotationMarks()} doesn't exist within Backlog with Id {backlogId.InQuotationMarks()}");

    public static ResultError GetItemNotFoundError(Guid backlogId, Guid itemId)
       => new(ErrorReason.ResourceNotFound, $"Item with Id {itemId.InQuotationMarks()} doesn't exist within Backlog with Id {backlogId.InQuotationMarks()}");
}
