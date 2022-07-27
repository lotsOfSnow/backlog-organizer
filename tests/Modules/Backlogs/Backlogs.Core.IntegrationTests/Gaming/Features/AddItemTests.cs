using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.AddItem;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Features;

public class AddBacklogItemTests : IClassFixture<BacklogsApplicationFactory>
{
    private readonly BacklogsApplicationFactory _factory;

    public AddBacklogItemTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
    {
        _factory = factory;
        _factory.TestOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Can_create_backlog_item()
    {
        const string expectedName = "Test item";
        var request = new AddBacklogItemCommand(GamingBacklog.InstanceId, expectedName);

        await _factory.SendAsync(request);

        var createdItem = await _factory.FindAsync<GameBacklogItem>(request.AddedItemId!);
        createdItem.Should().NotBeNull();
        createdItem!.Name.Should().Be(expectedName);
        createdItem.Id.Should().NotBe(default(Guid));
    }
}
