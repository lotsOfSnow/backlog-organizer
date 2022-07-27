using Ardalis.GuardClauses;
using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Models;

public abstract class BacklogItem : EntityBase
{
    public string Name { get; private set; }

    public BacklogItem(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    }

    public void UpdateName(string name)
        => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
}
