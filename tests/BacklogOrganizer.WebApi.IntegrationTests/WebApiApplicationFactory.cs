using BacklogOrganizer.Shared.Api.IntegrationTests;

namespace BacklogOrganizer.WebApi.IntegrationTests;

public sealed class WebApiApplicationFactory : CustomWebApplicationFactory<Program>
{
    public WebApiApplicationFactory()
        : base(null)
    {
    }
}
