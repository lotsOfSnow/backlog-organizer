using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace BacklogOrganizer.Shared.Core.Domain.Entities.Identity;
public class GuidIdProvider : IIdProvider<Guid>
{
    private readonly SequentialGuidValueGenerator _generator = new();

    public async Task<Guid> GetIdAsync(CancellationToken cancellationToken)
        => await _generator.NextAsync(null);
}
