using BacklogOrganizer.Modules.Backlogs.Core.Data.Mappings;
using BacklogOrganizer.Modules.Backlogs.Core.Data.Queries;
using BacklogOrganizer.Shared.Core.Mediator;
using BacklogOrganizer.Shared.Core.Results;
using Dapper;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems;

public record GetAssignmentsQuery(Guid BacklogId, Guid GroupId) : IQuery<Result<IEnumerable<GroupAssignmentDto>>>;

public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsQuery, Result<IEnumerable<GroupAssignmentDto>>>
{
    private readonly IQueryRepository _queryRepository;

    public GetAssignmentsQueryHandler(IQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<Result<IEnumerable<GroupAssignmentDto>>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Verify if backlog exists? check if it's even worth it for the queries (+ for displaying errors)
        var conn = await _queryRepository.ConnectionFactory.GetOrCreateConnectionAsync();

        var queryArgs = new { GroupId = request.GroupId };

        var groupQuerySql = _queryRepository.GetQuery("SELECT EXISTS(SELECT 1 FROM {0} WHERE {1} = @GroupId)", OrmMappings.Groups.Table, OrmMappings.Groups.Columns.Id);
        var groupExists = await conn.QuerySingleOrDefaultAsync<bool>(groupQuerySql, queryArgs);

        if (!groupExists)
        {
            return Result<IEnumerable<GroupAssignmentDto>>.Failure(BacklogResultErrors.GetGroupNotFoundError(request.BacklogId, request.GroupId));
        }

        var sql = _queryRepository.GetQuery("SELECT * FROM {0} WHERE {1} = @GroupId", OrmMappings.GroupAssignments.Table, OrmMappings.GroupAssignments.Columns.GroupId);

        var result = await conn.QueryAsync<GroupAssignmentDto>(sql, queryArgs);

        return Result<IEnumerable<GroupAssignmentDto>>.Success(result);
    }
}
