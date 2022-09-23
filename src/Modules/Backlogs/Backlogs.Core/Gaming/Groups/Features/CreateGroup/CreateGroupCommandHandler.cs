using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<GameBacklogItemsGroupDto>>
{
    private readonly IGamingBacklogRepository _repo;

    public CreateGroupCommandHandler(IGamingBacklogRepository repo) => _repo = repo;

    public async Task<Result<GameBacklogItemsGroupDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate that it found the right one.
        var backlog = await _repo.GetAsync(request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            return Result<GameBacklogItemsGroupDto>.Failure(GamingBacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var group = new GameBacklogItemsGroup(Guid.NewGuid(), backlog.Id, request.Name);

        backlog.AddGroup(group);

        await _repo.SaveChangesAsync(cancellationToken);

        var dto = new GameBacklogItemsGroupDto(group.Id, group.Name);
        return Result<GameBacklogItemsGroupDto>.Success(dto);
    }
}
