using Ardalis.GuardClauses;
using MediatR;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming.Features.AddItem;

public class AddBacklogItemCommandHandler : IRequestHandler<AddBacklogItemCommand>
{
    private readonly IGamingBacklogRepository _repository;

    public AddBacklogItemCommandHandler(IGamingBacklogRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddBacklogItemCommand request, CancellationToken cancellationToken)
    {
        // TODO: Support multiple backlogs, validate that it found the right one.
        var backlog = await _repository.GetAsync(GamingBacklog.InstanceId, cancellationToken);

        Guard.Against.Null(backlog, nameof(backlog));

        var item = new GameBacklogItem(request.Name);
        backlog.AddItem(item);

        await _repository.SaveChangesAsync(cancellationToken);

        request.AddedItemId = item.Id;

        return Unit.Value;
    }
}
