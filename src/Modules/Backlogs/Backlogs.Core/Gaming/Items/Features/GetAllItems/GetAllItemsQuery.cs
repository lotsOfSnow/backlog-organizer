using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;

public record GetAllItemsQuery(Guid BacklogId) : IRequest<Result<IEnumerable<BacklogItemDto>>>;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, Result<IEnumerable<BacklogItemDto>>>
{
    private readonly IBacklogStorage _storage;

    public GetAllItemsQueryHandler(IBacklogStorage storage)
        => _storage = storage;

    public async Task<Result<IEnumerable<BacklogItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var backlog = await _storage.Backlogs
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            return Result<IEnumerable<BacklogItemDto>>
                .Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var mappedItems = backlog
            .Items
            .Select(x => new BacklogItemDto(x.Id, x.Name))
            .ToList();

        return Result<IEnumerable<BacklogItemDto>>.Success(mappedItems);
    }
}
