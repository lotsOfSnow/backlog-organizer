using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;
using BacklogOrganizer.Shared.Core.Results;
using Dapper;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetDetails;
public record GetDetailsQuery(Guid BacklogId, Guid ItemId) : IRequest<Result<BacklogItemDetailsDto>>;

public class GetDetailsQueryHandler : IRequestHandler<GetDetailsQuery, Result<BacklogItemDetailsDto>>
{
    private readonly IQueryRepository _queryRepository;

    public GetDetailsQueryHandler(IQueryRepository queryRepository)
        => _queryRepository = queryRepository;

    public async Task<Result<BacklogItemDetailsDto>> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
    {
        var dbConnection = await _queryRepository.ConnectionFactory.GetOrCreateConnectionAsync();

        var backlogExists = await CommonQueries.BacklogExists(_queryRepository, dbConnection, request.BacklogId);
        if (!backlogExists)
        {
            return Result<BacklogItemDetailsDto>.Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var queryArgs = new { request.ItemId };

        var itemQuery = _queryRepository.GetQuery(
            "SELECT {0}, {1} " +
            "FROM {2} " +
            "WHERE {3} = @ItemId",
            OrmMappings.Items.Columns.Id, OrmMappings.Items.Columns.Name,
            OrmMappings.Items.Table,
            OrmMappings.Items.Columns.Id);

        var item = await dbConnection.QuerySingleOrDefaultAsync<BacklogItemDto>(itemQuery, queryArgs);
        if (item is null)
        {
            return Result<BacklogItemDetailsDto>.Failure(BacklogResultErrors.GetItemNotFoundError(request.BacklogId, request.ItemId));
        }

        var assignmentsQuery = _queryRepository.GetQuery(
            "SELECT {0} " +
            "FROM {1} " +
            "WHERE {2} = @ItemId",
            OrmMappings.GroupAssignments.Columns.GroupId,
            OrmMappings.GroupAssignments.Table,
            OrmMappings.GroupAssignments.Columns.ItemId);
        var groups = await dbConnection.QueryAsync<Guid>(assignmentsQuery, queryArgs);

        return Result<BacklogItemDetailsDto>.Success(new(item.Id, item.Name, groups));
    }
}
