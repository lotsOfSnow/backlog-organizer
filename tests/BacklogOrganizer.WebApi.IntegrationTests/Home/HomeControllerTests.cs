using System.Net;

namespace BacklogOrganizer.WebApi.IntegrationTests.Home;

public class HomeControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public HomeControllerTests(CustomWebApplicationFactory factory)
        => _factory = factory;

    [Fact]
    public async Task Returns_HTTP_OK()
    {
        const string url = "/";
        var client = _factory.CreateDefaultClient();

        var response = await client.GetAsync(url);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }
}
