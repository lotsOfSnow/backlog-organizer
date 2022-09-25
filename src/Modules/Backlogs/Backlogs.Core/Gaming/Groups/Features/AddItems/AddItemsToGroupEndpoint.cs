using System.Net.Mime;
using BacklogOrganizer.Modules.Backlogs.Core.Api;
using BacklogOrganizer.Shared.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;

[Route(ApiRoutes.BacklogGroups + "/{groupId}/items")]
[ApiVersion(ApiVersions.V1)]
public class AddItemsToGroupEndpoint : BaseController
{
    public AddItemsToGroupEndpoint(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Add existing items to a backlog group",
        OperationId = "addItemsToGamingBacklogGroup",
        Tags = new[] { ApiTags.BacklogGroups })]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> AddItemAsync(Guid backlogId, Guid groupId, AddItemsToGroupRequest request)
    {
        var command = new AddItemsToGroupCommand(backlogId, groupId, request.ItemIds);
        var result = await Mediator.Send(command);
        return result.ToNoContentResult();
    }
}
