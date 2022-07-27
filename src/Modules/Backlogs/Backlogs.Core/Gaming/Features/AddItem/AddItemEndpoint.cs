using BacklogOrganizer.Shared.Infrastructure.ApiControllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.AddItem;

[Route("backlogs/{id:guid}/gaming")]
public class AddItemEndpoint : BaseController
{
    public AddItemEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<Guid> AddItemAsync(Guid id, AddItemEndpointRequestContract request)
    {
        var command = request.CreateCommand(id);
        await Mediator.Send(command);
        return command.AddedItemId!.Value;
    }
}
