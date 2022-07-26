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
        var creationCommand = new AddBacklogItemCommand(Backlog.InstanceId, "Name");
        var creationResult = await _factory.SendAsync(creationCommand);

        const ItemCompletionStatus newStatus = ItemCompletionStatus.Completed;
        var command = new ChangeStatusCommand(Backlog.InstanceId, creationResult.Value!.Id, newStatus);
        await _factory.SendAsync(command);

        var item = await _factory.FindAsync<BacklogItem>(creationResult.Value.Id);
        item!.CompletionStatusDetails.Status.Should().Be(newStatus);
    }
}
