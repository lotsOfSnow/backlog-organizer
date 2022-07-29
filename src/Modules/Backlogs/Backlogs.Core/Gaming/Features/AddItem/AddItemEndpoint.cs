using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.AddItem;

[Route(ApiRoutes.GamingBacklogItems)]
public class AddItemEndpoint : BaseController
{
    public AddItemEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new backlog item",
        OperationId = "createGamingBacklogItem",
        Tags = new[] { ApiTags.GamingBacklogItems })]
    public async Task<Guid> AddItemAsync(AddItemEndpointRequestContract request)
    {
        var command = new AddBacklogItemCommand(request.BacklogId, request.Name);
        await Mediator.Send(command);
        return command.AddedItemId!.Value;
    }
}
