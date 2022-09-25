using BacklogOrganizer.Shared.Core.Domain.Entities.Identity;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<BacklogGroupDto>>
{
    private readonly IBacklogRepository _repo;
    private readonly IIdProvider<Guid> _guidProvider;

    public CreateGroupCommandHandler(IBacklogRepository repo, IIdProvider<Guid> guidProvider)
    {
        _repo = repo;
        _guidProvider = guidProvider;
    }

    public async Task<Result<BacklogGroupDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate that it found the right one.
        var backlog = await _repo.GetAsync(request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            return Result<BacklogGroupDto>.Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var id = await _guidProvider.GetIdAsync(cancellationToken);
        var group = new BacklogGroup(id, backlog.Id, request.Name);

        backlog.AddGroup(group);

        await _repo.SaveChangesAsync(cancellationToken);

        var dto = new BacklogGroupDto(group.Id, group.Name);
        return Result<BacklogGroupDto>.Success(dto);
    }
}
