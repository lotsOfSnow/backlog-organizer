using Ardalis.GuardClauses;
using BacklogOrganizer.Shared.Core.Domain.Entities.Identity;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;

public class AddBacklogItemCommandHandler : IRequestHandler<AddBacklogItemCommand>
{
    private readonly IBacklogRepository _repository;
    private readonly IIdProvider<Guid> _guidProvider;

    public AddBacklogItemCommandHandler(IBacklogRepository repository, IIdProvider<Guid> guidProvider)
    {
        _repository = repository;
        _guidProvider = guidProvider;
    }

    public async Task<Unit> Handle(AddBacklogItemCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate that it found the right one.
        var backlog = await _repository.GetAsync(request.BacklogId, cancellationToken);

        Guard.Against.Null(backlog, nameof(backlog));

        var id = await _guidProvider.GetIdAsync(cancellationToken);
        var item = new BacklogItem(id, request.Name);
        backlog.AddItem(item);

        await _repository.SaveChangesAsync(cancellationToken);

        request.AddedItemId = item.Id;

        return Unit.Value;
    }
}
