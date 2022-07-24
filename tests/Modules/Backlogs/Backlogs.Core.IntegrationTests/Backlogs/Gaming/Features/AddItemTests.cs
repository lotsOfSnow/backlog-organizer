using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Backlogs.Gaming.Features.AddItem;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Backlogs.Gaming.Features;

public class AddBacklogItemTests : IClassFixture<BacklogsApplicationFactory>
{
    private readonly BacklogsApplicationFactory _factory;

    public AddBacklogItemTests(BacklogsApplicationFactory factory)
        => _factory = factory;

    [Fact]
    public async Task Can_create_backlog_item()
    {
        var request = new AddBacklogItemCommand(GamingBacklog.InstanceId, "Test item");

        await _factory.SendAsync(request);

        var createdItem = await _factory.FindAsync<GameBacklogItem>(new { request.AddedItemId });
    }
}
