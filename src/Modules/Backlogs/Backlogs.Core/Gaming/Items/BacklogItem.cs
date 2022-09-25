using Ardalis.GuardClauses;
using BacklogOrganizer.Modules.Backlogs.Core.Models;
using BacklogOrganizer.Shared.Core.Domain.Entities;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;

public class BacklogItem : GuidIdEntity
{
    public BacklogItem(Guid id, string name)
        : base(id)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        CompletionStatusDetails = new(ItemCompletionStatus.ToDo);
    }

    public string Name { get; private set; }

    public ItemCompletionStatusDetails CompletionStatusDetails { get; private set; }

    public void UpdateName(string name)
        => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

    public void SetCompletionStatus(ItemCompletionStatus status)
    {
        CompletionStatusDetails = new(status);
    }
}
