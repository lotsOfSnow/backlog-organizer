using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;
using BacklogOrganizer.Shared.Core.Domain.Entities.Identity;
using BacklogOrganizer.Shared.Core.Results;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;

public class AddBacklogItemCommandHandler : IRequestHandler<AddBacklogItemCommand, Result<BacklogItemDto>>
{
    private readonly IBacklogRepository _repository;
    private readonly IIdProvider<Guid> _guidProvider;

    public AddBacklogItemCommandHandler(IBacklogRepository repository, IIdProvider<Guid> guidProvider)
    {
        _repository = repository;
        _guidProvider = guidProvider;
    }

    public async Task<Result<BacklogItemDto>> Handle(AddBacklogItemCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate that it found the right one.
        var backlog = await _repository.GetAsync(request.BacklogId, cancellationToken);

        if (backlog is null)
        {
            return Result<BacklogItemDto>.Failure(BacklogResultErrors.GetBacklogNotFoundError(request.BacklogId));
        }

        var id = await _guidProvider.GetIdAsync(cancellationToken);
        var item = new BacklogItem(id, request.Name);
        backlog.AddItem(item);

        await _repository.SaveChangesAsync(cancellationToken);

        var dto = new BacklogItemDto(item.Id, item.Name);

        return Result<BacklogItemDto>.Success(dto);
    }
}
