using System.Net.Mime;
using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetDetails;

[Route(ApiRoutes.Backlogs + "/{id:guid}/items/{itemId:guid}")]
[ApiVersion(ApiVersions.V1)]
public class GetDetailsEndpoint : BaseController
{
    public GetDetailsEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get details of an item",
        OperationId = "getGamingBacklogItemDetails",
        Tags = new[] { ApiTags.BacklogItems })]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<BacklogItemDetailsDto>> GetDetailsAsync(Guid id, Guid itemId)
    {
        var command = new GetDetailsQuery(id, itemId);
        var result = await Mediator.Send(command);
        return result.ToObjectResult();
    }
}
