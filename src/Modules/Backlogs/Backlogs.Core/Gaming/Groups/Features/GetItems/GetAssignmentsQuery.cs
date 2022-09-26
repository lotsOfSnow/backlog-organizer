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
    private readonly IQueryDbConnectionFactory _queryDbConnectionFactory;

    public GetAssignmentsQueryHandler(IQueryDbConnectionFactory queryDbConnectionFactory)
        => _queryDbConnectionFactory = queryDbConnectionFactory;

    public async Task<Result<IEnumerable<GroupAssignmentDto>>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Verify if backlog exists? check if it's even worth it for the queries (+ for displaying errors)
        var conn = await _queryDbConnectionFactory.GetOrCreateConnectionAsync();

        var queryArgs = new { GroupId = request.GroupId };

        var groupQuerySql = _queryDbConnectionFactory.BuildQuery("SELECT EXISTS(SELECT 1 FROM {0} WHERE {1} = @GroupId)", OrmMappings.Groups.Table, OrmMappings.Groups.Columns.Id);
        var groupExists = await conn.QuerySingleOrDefaultAsync<bool>(groupQuerySql, queryArgs);

        if (!groupExists)
        {
            return Result<IEnumerable<GroupAssignmentDto>>.Failure(BacklogResultErrors.GetGroupNotFoundError(request.BacklogId, request.GroupId));
        }

        var sql = _queryDbConnectionFactory.BuildQuery("SELECT * FROM {0} WHERE {1} = @GroupId", OrmMappings.GroupAssignments.Table, OrmMappings.GroupAssignments.Columns.GroupId);

        //System.InvalidOperationException: A parameterless default constructor or one matching signature (System.Guid GroupId, System.Guid ItemId) is required for BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems.GroupAssignmentDto materialization
        var result = await conn.QueryAsync<GroupAssignmentDto>(sql, queryArgs);

        return Result<IEnumerable<GroupAssignmentDto>>.Success(result);
    }
}
