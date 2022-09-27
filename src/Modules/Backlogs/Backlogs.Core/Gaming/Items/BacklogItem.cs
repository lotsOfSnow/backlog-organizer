using Ardalis.GuardClauses;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
using BacklogOrganizer.Modules.Backlogs.Core.Models;
using BacklogOrganizer.Shared.Core.Domain.Entities;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;

public class BacklogItem : GuidIdEntity
{
    private readonly HashSet<BacklogItemPlatform> _platforms = new();

    public BacklogItem(Guid id, string name, IEnumerable<Platform>? platforms = null)
        : base(id)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        CompletionStatusDetails = new(ItemCompletionStatus.ToDo);

        if (platforms is null)
        {
            return;
        }

        foreach (var platform in platforms)
        {
            AddPlatform(platform);
        }
    }

    public string Name { get; private set; }

    public ItemCompletionStatusDetails CompletionStatusDetails { get; private set; }

    public void UpdateName(string name)
        => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

    public void AddPlatform(Platform platform)
        => _platforms.Add(new(Id, platform.Id));

    public void RemovePlatform(Guid id)
    {
        var platform = _platforms.SingleOrDefault(x => x.PlatformId == id);
        if (platform is null)
        {
            return;
        }

        _platforms.Remove(platform);
        AddDomainEvent(new PlatformUnassignedDomainEvent(platform.ItemId, platform.PlatformId));
    }

    public void SetCompletionStatus(ItemCompletionStatus status)
    {
        CompletionStatusDetails = new(status);
    }
}
