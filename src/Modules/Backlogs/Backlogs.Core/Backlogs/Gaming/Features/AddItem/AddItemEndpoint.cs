using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming.Features.AddItem;

[Route("backlogs/{id:guid}/gaming")]
public class AddItemEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public AddItemEndpoint(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    public async Task<Guid> AddItemAsync(Guid id, AddItemEndpointRequestContract request)
    {
        var command = request.CreateCommand(id);
        await _mediator.Send(command);
        return command.AddedItemId!.Value;
    }
}
