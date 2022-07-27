using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.Shared.Infrastructure.ApiControllers;

[Route("/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator { get; }

    protected BaseController(IMediator mediator)
        => Mediator = mediator;
}
