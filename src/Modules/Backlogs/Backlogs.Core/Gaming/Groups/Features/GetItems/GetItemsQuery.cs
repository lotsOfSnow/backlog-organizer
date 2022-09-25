using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Shared.Core.Mediator;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems;

public record GetItemsQuery(Guid GroupId) : IQuery<Result<IEnumerable<GroupAssignmentDto>>>;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, Result<IEnumerable<GroupAssignmentDto>>>
{
    private readonly IQueryDbConnectionFactory _queryDbConnectionFactory;

    public GetItemsQueryHandler(IQueryDbConnectionFactory queryDbConnectionFactory)
        => _queryDbConnectionFactory = queryDbConnectionFactory;

    public async Task<Result<IEnumerable<GroupAssignmentDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var conn = await _queryDbConnectionFactory.CreateNewConnectionAsync();
        throw new NotImplementedException();
    }
}
