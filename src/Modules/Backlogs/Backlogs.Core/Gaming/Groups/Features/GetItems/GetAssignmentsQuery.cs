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
        var dbConnection = await _queryRepository.ConnectionFactory.GetOrCreateConnectionAsync();

        var backlogQueryArgs = new { request.BacklogId };
        var backlogExistenceQuery = _queryRepository.GetExistenceCheckQuery(OrmMappings.Backlogs.Table, OrmMappings.Backlogs.Columns.Id, nameof(backlogQueryArgs.BacklogId));
        var backlogExists = await dbConnection.QuerySingleOrDefaultAsync<bool>(backlogExistenceQuery, backlogQueryArgs);
        if (!backlogExists)
        {
            return Result<IEnumerable<GroupAssignmentDto>>.Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var groupQueryArgs = new { request.GroupId };
        var groupExistenceQuery = _queryRepository.GetExistenceCheckQuery(OrmMappings.Groups.Table, OrmMappings.Groups.Columns.Id, nameof(groupQueryArgs.GroupId));
        var groupExists = await dbConnection.QuerySingleOrDefaultAsync<bool>(groupExistenceQuery, groupQueryArgs);
        if (!groupExists)
        {
            return Result<IEnumerable<GroupAssignmentDto>>.Failure(BacklogResultErrors.GetGroupNotFoundError(request.BacklogId, request.GroupId));
        }

        var assignmentsQuery = _queryRepository.GetQuery("SELECT * FROM {0} WHERE {1} = @GroupId", OrmMappings.GroupAssignments.Table, OrmMappings.GroupAssignments.Columns.GroupId);
        var result = await dbConnection.QueryAsync<GroupAssignmentDto>(assignmentsQuery, groupQueryArgs);
        return Result<IEnumerable<GroupAssignmentDto>>.Success(result);
    }
}
