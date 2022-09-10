using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.GetAllItems;

[Route(ApiRoutes.GamingBacklogItems + "/{id:guid}")]
[ApiVersion(ApiVersions.V1)]
public class GetAllItemsEndpoint : BaseController
{
    public GetAllItemsEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all items of a backlog",
        OperationId = "getAllGamingBacklogItems",
        Tags = new[] { ApiTags.GamingBacklogItems })]
    public async Task<ActionResult<IEnumerable<GameBacklogItemDto>>> GetAllItems(Guid id)
    {
        var command = new GetAllItemsQuery(id);
        var result = await Mediator.Send(command);
        return result.ToObjectResult();
    }
}
