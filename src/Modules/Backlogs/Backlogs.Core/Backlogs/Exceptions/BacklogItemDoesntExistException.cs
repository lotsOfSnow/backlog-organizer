namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Exceptions;

public class BacklogItemDoesntExistException : Exception
{
    public BacklogItemDoesntExistException(Guid backlogId, Guid itemId)
        : base($"Backlog {backlogId} doesn't contain item with id {itemId}")
    {
    }
}
