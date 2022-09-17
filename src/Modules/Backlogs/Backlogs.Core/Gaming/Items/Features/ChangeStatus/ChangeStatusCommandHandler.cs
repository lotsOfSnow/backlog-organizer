using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.ChangeStatus;

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand>
{
    private readonly IGamingBacklogRepository _repository;
    private readonly ILogger<ChangeStatusCommandHandler> _logger;

    public ChangeStatusCommandHandler(IGamingBacklogRepository repository, ILogger<ChangeStatusCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var backlog = await _repository.GetAsync(request.BacklogId, cancellationToken);

        Guard.Against.Null(backlog, nameof(backlog));

        var item = backlog.Items.FirstOrDefault(x => x.Id == request.ItemId);

        Guard.Against.Null(item, nameof(item));

        item.SetCompletionStatus(request.NewStatus);

        await _repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
