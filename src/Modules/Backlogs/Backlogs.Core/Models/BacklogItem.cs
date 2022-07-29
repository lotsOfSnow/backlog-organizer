using Ardalis.GuardClauses;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Models;

public abstract class BacklogItem : EntityBase
{
    public string Name { get; private set; }

    public ItemCompletionStatusDetails CompletionStatusDetails { get; private set; }

    protected BacklogItem(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        CompletionStatusDetails = new(ItemCompletionStatus.ToDo);
    }

    public void UpdateName(string name)
        => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

    public void SetCompletionStatus(ItemCompletionStatus status)
    {
        CompletionStatusDetails = new(status);
    }
}
