using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.WebApi.Controllers;

[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public string Get()
        => "Backlog Organizer API";
}
