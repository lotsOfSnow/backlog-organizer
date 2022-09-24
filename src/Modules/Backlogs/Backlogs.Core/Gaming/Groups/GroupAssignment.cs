using BacklogOrganizer.Shared.Core;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;

public class GroupAssignment : Entity, IEquatable<GroupAssignment?>
{
    private GroupAssignment(Guid groupId, Guid itemId)
    {
        GroupId = groupId;
        ItemId = itemId;
    }

    public Guid GroupId { get; }

    public Guid ItemId { get; }

    public static GroupAssignment Create(Guid groupId, Guid itemId)
        => new(groupId, itemId);

    public override bool Equals(object? obj)
        => Equals(obj as GroupAssignment);

    public bool Equals(GroupAssignment? other)
        => other is not null
        && GroupId.Equals(other.GroupId)
        && ItemId.Equals(other.ItemId);

    public override int GetHashCode()
        => HashCode.Combine(GroupId, ItemId);
}
