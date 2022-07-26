namespace BacklogOrganizer.Shared.Core;

public interface ICommandSideRepository<TAggregateRoot>
    where TAggregateRoot : IAggregateRoot
{
    Task<TAggregateRoot?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
