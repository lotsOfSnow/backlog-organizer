using System.Net.Mime;
using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.GetAllGroups;
[Route(ApiRoutes.BacklogGroups)]
[ApiVersion(ApiVersions.V1)]

public class GetAllGroupsEndpoint : BaseController
{
    public GetAllGroupsEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all groups of a backlog",
        OperationId = "getAllGamingBacklogGroups",
        Tags = new[] { ApiTags.BacklogGroups })]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<BacklogGroupDto>>> GetAssignmentsAsync(Guid backlogId)
    {
        var query = new GetAllGroupsQuery(backlogId);
        var result = await Mediator.Send(query);
        return result.ToObjectResult();
    }
}
