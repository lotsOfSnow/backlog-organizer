using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using BacklogOrganizer.Shared.Core.Results;
using Dapper;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;

public record GetAllItemsQuery(Guid BacklogId) : IRequest<Result<IEnumerable<BacklogItemDto>>>;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, Result<IEnumerable<BacklogItemDto>>>
{
    private readonly IQueryRepository _queryRepository;

    public GetAllItemsQueryHandler(IQueryRepository queryRepository)
        => _queryRepository = queryRepository;

    public async Task<Result<IEnumerable<BacklogItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var dbConnection = await _queryRepository.ConnectionFactory.GetOrCreateConnectionAsync();
        var backlogExists = await CommonQueries.BacklogExists(_queryRepository, dbConnection, request.BacklogId);

        if (!backlogExists)
        {
            return Result<IEnumerable<BacklogItemDto>>
                .Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var args = new { request.BacklogId };
        var query = _queryRepository
            .GetQuery("SELECT {0}, {1} " +
                "FROM {2} " +
                $"WHERE {{3}} = @{nameof(args.BacklogId)}",
            OrmMappings.Items.Columns.Id, OrmMappings.Items.Columns.Name,
            OrmMappings.Items.Table,
            OrmMappings.Items.Columns.BacklogId);
        var backlogItems = await dbConnection.QueryAsync<BacklogItemDto>(query, args);

        return Result<IEnumerable<BacklogItemDto>>.Success(backlogItems);
    }
}
