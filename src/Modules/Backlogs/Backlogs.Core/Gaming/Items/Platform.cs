using BacklogOrganizer.Shared.Core.Domain.Entities;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
public sealed class Platform : GuidIdEntity
{
    public string Name { get; }

    public bool IsDefault { get; }

    public static Platform Default(Guid id, string name)
        => new(id, name, true);

    public static Platform Custom(Guid id, string name)
        => new(id, name, false);

    private Platform(Guid id, string name, bool isDefault) : base(id)
    {
        Name = name;
        IsDefault = isDefault;
    }
}
