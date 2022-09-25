namespace BacklogOrganizer.Shared.Core.Domain.Entities;

public abstract class GuidIdEntity : Entity, IEquatable<GuidIdEntity>
{
    public Guid Id { get; private set; }

    protected GuidIdEntity(Guid id)
    {
        Id = id;
    }

    public virtual bool Equals(GuidIdEntity? other)
        => other?.Id == Id;

    public override bool Equals(object? obj)
        => Equals(obj as GuidIdEntity);

    public override int GetHashCode()
        => Id.GetHashCode();
}
