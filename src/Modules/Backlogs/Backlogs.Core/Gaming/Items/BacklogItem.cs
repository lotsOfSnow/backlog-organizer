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
            AddPlatform(platform.Id);
        }
    }

    public string Name { get; private set; }

    public ItemCompletionStatusDetails CompletionStatusDetails { get; private set; }

    public void UpdateName(string name)
        => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

    public void UpdatePlatforms(IEnumerable<Platform> newPlatforms)
    {
        var currentPlatformIds = _platforms.Select(x => x.PlatformId).ToList();
        var newPlatformIds = newPlatforms.Select(x => x.Id);
        var toAdd = newPlatformIds.Except(currentPlatformIds);
        var toRemove = currentPlatformIds.Except(newPlatformIds);

        foreach (var platformId in toAdd)
        {
            AddPlatform(platformId);
        }

        foreach (var platformId in toRemove)
        {
            _platforms.RemoveWhere(x => x.PlatformId == platformId);
            AddDomainEvent(new PlatformUnassignedDomainEvent(Id, platformId));
        }
    }

    public void SetCompletionStatus(ItemCompletionStatus status)
    {
        CompletionStatusDetails = new(status);
    }

    private void AddPlatform(Guid id)
    {
        _platforms.Add(new(Id, id));
    }
}
