using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.ChangeStatus;
using BacklogOrganizer.Modules.Backlogs.Core.Models;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Items.Features;

public class ChangeStatusTests : IClassFixture<BacklogsApplicationFactory>
{
    private readonly BacklogsApplicationFactory _factory;

    public ChangeStatusTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
    {
        _factory = factory;
        _factory.TestOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Updates_status()
    {
        var creationCommand = new AddBacklogItemCommand(GamingBacklog.InstanceId, "Name");
        await _factory.SendAsync(creationCommand);

        var newStatus = ItemCompletionStatus.Completed;
        var command = new ChangeStatusCommand(GamingBacklog.InstanceId, creationCommand.AddedItemId!.Value, newStatus);
        await _factory.SendAsync(command);

        var item = await _factory.FindAsync<GameBacklogItem>(creationCommand.AddedItemId);
        item!.CompletionStatusDetails.Status.Should().Be(newStatus);
    }
}
