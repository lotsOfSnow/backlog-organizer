using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Shared.Core.Results;
using BacklogOrganizer.Shared.Core.Results.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.GetAllItems;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, Result<IEnumerable<GameBacklogItemDto>>>
{
    private readonly IBacklogStorage _storage;
    private readonly ILogger<GetAllItemsQueryHandler> _logger;

    public GetAllItemsQueryHandler(IBacklogStorage storage, ILogger<GetAllItemsQueryHandler> logger)
    {
        _storage = storage;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<GameBacklogItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var backlog = await _storage.GamingBacklogs
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            _logger.LogInformation("No backlog with Id {RequestBacklogId} found", request.BacklogId);
            return Result<IEnumerable<GameBacklogItemDto>>
                .Failure(new(ErrorReason.ResourceNotFound, $"Backlog with Id {request.BacklogId} doesn't exist"));
        }

        var mappedItems = backlog
            .Items
            .Select(x => new GameBacklogItemDto(x.Id, x.Name))
            .ToList();

        return Result<IEnumerable<GameBacklogItemDto>>.Success(mappedItems);
    }
}
