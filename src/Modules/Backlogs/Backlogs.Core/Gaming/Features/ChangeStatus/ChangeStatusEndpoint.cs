using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.ChangeStatus;

[Route(ApiRoutes.GamingBacklogItems + "/{id:guid}" + "/change-status")]
[ApiVersion("1.0")]
public class ChangeStatusEndpoint : BaseController
{
    public ChangeStatusEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Change status of a backlog item",
        OperationId = "changeGamingBacklogItemStatus",
        Tags = new[] { ApiTags.GamingBacklogItems })]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<ActionResult> ChangeStatus(Guid id, [FromForm] ChangeStatusEndpointRequest request)
    {
        var command = new ChangeStatusCommand(request.BacklogId, id, request.NewStatus);
        await Mediator.Send(command);
        return NoContent();
    }
}
