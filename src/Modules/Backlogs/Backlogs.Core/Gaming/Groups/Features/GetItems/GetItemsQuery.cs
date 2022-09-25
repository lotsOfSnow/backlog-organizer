using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems;

public record GetItemsQuery(Guid GroupId) : IRequest<Result<IEnumerable<GroupAssignmentDto>>>;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, Result<IEnumerable<GroupAssignmentDto>>>
{
    public Task<Result<IEnumerable<GroupAssignmentDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
