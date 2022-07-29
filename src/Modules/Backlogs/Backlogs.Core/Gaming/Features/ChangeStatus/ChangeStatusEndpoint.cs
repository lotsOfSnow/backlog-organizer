using BacklogOrganizer.Shared.Infrastructure.ApiControllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.ChangeStatus;

[Route(ApiRoutes.GamingBacklogItems + "/{id:guid}" + "/change-status")]
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
    public async Task<ActionResult> ChangeStatus(Guid id, ChangeStatusEndpointRequest request)
    {
        var command = new ChangeStatusCommand(request.BacklogId, id, request.NewStatus);
        await Mediator.Send(command);
        return NoContent();
    }
}
