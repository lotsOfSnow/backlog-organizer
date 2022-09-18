using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, Result<IEnumerable<GameBacklogItemDto>>>
{
    private readonly IBacklogStorage _storage;

    public GetAllItemsQueryHandler(IBacklogStorage storage)
        => _storage = storage;

    public async Task<Result<IEnumerable<GameBacklogItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var backlog = await _storage.GamingBacklogs
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            return Result<IEnumerable<GameBacklogItemDto>>
                .Failure(GamingBacklogResultErrors.BacklogNotFound(request.BacklogId));
        }

        var mappedItems = backlog
            .Items
            .Select(x => new GameBacklogItemDto(x.Id, x.Name))
            .ToList();

        return Result<IEnumerable<GameBacklogItemDto>>.Success(mappedItems);
    }
}
