using System.Net.Mime;
using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.ChangeStatus;

[Route(ApiRoutes.BacklogItems + "/{id:guid}/change-status")]
[ApiVersion(ApiVersions.V1)]
public class ChangeStatusEndpoint : BaseController
{
    public ChangeStatusEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Change status of a backlog item",
        OperationId = "changeGamingBacklogItemStatus",
        Tags = new[] { ApiTags.BacklogItems })]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> ChangeStatus(Guid id, [FromForm] ChangeStatusEndpointRequest request)
    {
        var command = new ChangeStatusCommand(request.BacklogId, id, request.NewStatus);
        await Mediator.Send(command);
        return NoContent();
    }
}
