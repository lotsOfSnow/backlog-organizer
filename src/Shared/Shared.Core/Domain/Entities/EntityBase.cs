namespace BacklogOrganizer.Shared.Core.Domain.Entities;

public class EntityBase : Entity, IEquatable<EntityBase>
{
    public Guid Id { get; set; }

    public bool Equals(EntityBase? other)
        => other?.Id == Id;

    public override bool Equals(object? obj)
        => Equals(obj as EntityBase);

    public override int GetHashCode()
        => Id.GetHashCode();
}
