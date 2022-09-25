using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Exceptions;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;

public record AddItemsToGroupCommand(Guid BacklogId, Guid GroupId, IEnumerable<Guid> Items) : IRequest<Result<Unit>>;

public class AddItemsToGroupCommandHandler : IRequestHandler<AddItemsToGroupCommand, Result<Unit>>
{
    private readonly IGamingBacklogRepository _repository;

    public AddItemsToGroupCommandHandler(IGamingBacklogRepository repository) => _repository = repository;

    public async Task<Result<Unit>> Handle(AddItemsToGroupCommand request, CancellationToken cancellationToken)
    {
        var backlog = await _repository.GetAsync(request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            return Result<Unit>.Failure(GamingBacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        try
        {
            backlog.AddItemsToGroup(request.GroupId, request.Items);
        }
        catch (GroupNotFoundException)
        {
            return Result<Unit>.Failure(GamingBacklogResultErrors.GetGroupNotFoundError(request.BacklogId, request.GroupId));
        }

        await _repository.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
