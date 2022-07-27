using System.Net;
using Xunit.Abstractions;

namespace BacklogOrganizer.WebApi.IntegrationTests.Home;

public class HomeControllerTests : IClassFixture<WebApiApplicationFactory>
{
    private readonly WebApiApplicationFactory _factory;

    public HomeControllerTests(ITestOutputHelper testOutputHelper, WebApiApplicationFactory factory)
    {
        _factory = factory;
        _factory.TestOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Returns_HTTP_OK()
    {
        const string url = "/";
        var client = _factory.CreateDefaultClient();

        var response = await client.GetAsync(url);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }
}
