using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Data;

public interface IBacklogStorage
{
    DbSet<Backlog> Backlogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
