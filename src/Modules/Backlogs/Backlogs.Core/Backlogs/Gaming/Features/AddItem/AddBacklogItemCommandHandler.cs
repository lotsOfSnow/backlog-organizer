using Ardalis.GuardClauses;
using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming.Features.AddItem;

public class AddBacklogItemCommandHandler : IRequestHandler<AddBacklogItemCommand>
{
    private readonly IBacklogStorage _storage;

    public AddBacklogItemCommandHandler(IBacklogStorage storage)
    {
        _storage = storage;
    }

    public async Task<Unit> Handle(AddBacklogItemCommand request, CancellationToken cancellationToken)
    {
        // TODO: Support multiple backlogs, validate that it found the right one.
        var backlog = _storage
            .GamingBacklogs
            .Include(x => x.Items)
            .FirstOrDefault();

        Guard.Against.Null(backlog, nameof(backlog));

        var item = new GameBacklogItem(request.Name);
        backlog.AddItem(item);

        await _storage.SaveChangesAsync(cancellationToken);

        request.AddedItemId = item.Id;

        return Unit.Value;
    }
}
