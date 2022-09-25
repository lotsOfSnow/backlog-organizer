namespace BacklogOrganizer.Shared.Core.Domain.Entities.Identity;
public interface IIdProvider<TValue>
    where TValue : struct
{
    Task<TValue> GetIdAsync(CancellationToken cancellationToken);
}
