using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Shared.Core.Mediator;
using BacklogOrganizer.Shared.Core.Results;
using Dapper;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.GetAllGroups;

public record GetAllGroupsQuery(Guid BacklogId) : IQuery<Result<IEnumerable<BacklogGroupDto>>>;

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<BacklogGroupDto>>>
{
    private readonly IQueryRepository _queryRepository;

    public GetAllGroupsQueryHandler(IQueryRepository queryRepository)
        => _queryRepository = queryRepository;

    public async Task<Result<IEnumerable<BacklogGroupDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var dbConnection = await _queryRepository.ConnectionFactory.GetOrCreateConnectionAsync();

        var backlogExists = await CommonQueries.BacklogExists(_queryRepository, dbConnection, request.BacklogId);
        if (!backlogExists)
        {
            return Result<IEnumerable<BacklogGroupDto>>.Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var backlogGroupsQuery = _queryRepository.GetQuery("SELECT * FROM {0} WHERE {1} = @BacklogId", OrmMappings.Groups.Table, OrmMappings.Groups.Columns.BacklogId);

        var result = await dbConnection.QueryAsync<BacklogGroupDto>(backlogGroupsQuery, new { request.BacklogId });

        return Result<IEnumerable<BacklogGroupDto>>.Success(result);
    }
}
