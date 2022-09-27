using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
using BacklogOrganizer.Shared.Core.Domain.Entities;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
public class BacklogItemPlatform : Entity, IEquatable<BacklogItemPlatform?>
{
    public BacklogItemPlatform(Guid itemId, Guid platformId)
    {
        ItemId = itemId;
        PlatformId = platformId;
        AddDomainEvent(new NewPlatformAssignedDomainEvent(ItemId, PlatformId));
    }

    public Guid ItemId { get; set; }

    public Guid PlatformId { get; set; }

    public override bool Equals(object? obj)
        => Equals(obj as BacklogItemPlatform);

    public bool Equals(BacklogItemPlatform? other)
        => other is not null && ItemId.Equals(other.ItemId) && PlatformId.Equals(other.PlatformId);

    public override int GetHashCode()
        => HashCode.Combine(ItemId, PlatformId);
}
