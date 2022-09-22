using BacklogOrganizer.Shared.Core.Exceptions;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
public class GroupAlreadyExistsException : CustomException
{
    public string Name { get; }

    public GroupAlreadyExistsException(string name)
    {
        Name = name;
    }
}
