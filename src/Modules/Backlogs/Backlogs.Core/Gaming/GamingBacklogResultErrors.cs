using BacklogOrganizer.Shared.Core.Results.Errors;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;
public static class GamingBacklogResultErrors
{
    public static ResultError BacklogNotFound(Guid id)
        => new(ErrorReason.ResourceNotFound, $"Backlog with Id \"{id}\" doesn't exist");
}
