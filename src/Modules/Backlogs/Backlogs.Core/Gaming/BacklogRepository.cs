using BacklogOrganizer.Modules.Backlogs.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming;

public class BacklogRepository : IBacklogRepository
{
    private readonly IBacklogStorage _storage;

    public BacklogRepository(IBacklogStorage storage)
        => _storage = storage;

    public async Task<Backlog?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _storage
            .Backlogs
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _storage.SaveChangesAsync(cancellationToken);
}
