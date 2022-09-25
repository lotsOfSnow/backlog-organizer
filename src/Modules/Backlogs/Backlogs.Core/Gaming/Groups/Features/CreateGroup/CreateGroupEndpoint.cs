using System.Net.Mime;
using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.CreateGroup;

[Route(ApiRoutes.BacklogGroups)]
[ApiVersion(ApiVersions.V1)]
public class CreateGroupEndpoint : BaseController
{
    public CreateGroupEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new backlog group",
        OperationId = "createGamingBacklogGroup",
        Tags = new[] { ApiTags.BacklogGroups })]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<BacklogGroupDto>> AddItemAsync(Guid id, CreateGroupRequestContract request)
    {
        var command = new CreateGroupCommand(id, request.Name);
        var group = await Mediator.Send(command);
        return group.ToObjectResult();
    }
}
