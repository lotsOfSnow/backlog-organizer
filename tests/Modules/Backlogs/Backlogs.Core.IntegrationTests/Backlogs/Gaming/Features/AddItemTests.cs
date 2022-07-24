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
        const string expectedName = "Test item";
        var request = new AddBacklogItemCommand(GamingBacklog.InstanceId, expectedName);

        await _factory.SendAsync(request);

        var createdItem = await _factory.FindAsync<GameBacklogItem>(request.AddedItemId!);
        createdItem.Should().NotBeNull();
        createdItem!.Name.Should().Be(expectedName);
        createdItem.Id.Should().NotBe(default(Guid));
    }
}
