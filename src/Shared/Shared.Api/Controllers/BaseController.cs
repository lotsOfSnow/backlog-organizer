using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.Shared.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator { get; }

    protected BaseController(IMediator mediator)
        => Mediator = mediator;
}
