using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.AddItem;

[Route(ApiRoutes.GamingBacklogItems)]
[ApiVersion(ApiVersions.V1)]
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
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<Guid> AddItemAsync([FromForm] AddItemEndpointRequestContract request)
    {
        var command = new AddBacklogItemCommand(request.BacklogId, request.Name);
        await Mediator.Send(command);
        return command.AddedItemId!.Value;
    }
}
