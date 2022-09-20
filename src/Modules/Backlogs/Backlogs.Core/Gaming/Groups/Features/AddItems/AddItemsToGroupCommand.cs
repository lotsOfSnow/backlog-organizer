using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;

public record AddItemsToGroupCommand(Guid BacklogId, Guid GroupId, ICollection<Guid> Items) : IRequest<Result<Unit>>;

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

        var group = backlog.Groups.FirstOrDefault(x => x.Id == request.GroupId);

        if (group is null)
        {
            return Result<Unit>.Failure(GamingBacklogResultErrors.GetGroupNotFoundError(request.BacklogId, request.GroupId));
        }

        var items = backlog.Items.Where(x => request.Items.Contains(x.Id)).ToArray();

        group.AddItems(items);

        await _repository.SaveChangesAsync();

        return Result<Unit>.Success(Unit.Value);
    }
}
