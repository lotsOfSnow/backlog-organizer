using System.Net.Mime;
using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems;

[Route(ApiRoutes.BacklogGroups + "/{groupId}/items")]
[ApiVersion(ApiVersions.V1)]
public class GetItemsEndpoint : BaseController
{
    public GetItemsEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get items assigned to group",
        OperationId = "getAssignmentsOfGamingBacklogGroup",
        Tags = new[] { ApiTags.BacklogGroups })]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<GroupAssignmentDto>>> GetAssignmentsAsync(Guid backlogId, Guid groupId)
    {
        var command = new GetItemsQuery(groupId);
        var result = await Mediator.Send(command);
        return result.ToObjectResult();
    }
}
