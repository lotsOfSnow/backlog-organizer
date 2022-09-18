namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
public class GroupAlreadyExistsException : Exception
{
    public GroupAlreadyExistsException() : base()
    {
    }

    public GroupAlreadyExistsException(string? message) : base(message)
    {
    }

    public GroupAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
