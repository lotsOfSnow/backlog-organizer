using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Data;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming;

public class GamingBacklogRepository : IGamingBacklogRepository
{
    private readonly IBacklogStorage _storage;

    public GamingBacklogRepository(IBacklogStorage storage)
        => _storage = storage;

    public async Task<GamingBacklog?> GetAsync(Guid id)
        => await _storage
            .GamingBacklogs
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
}
