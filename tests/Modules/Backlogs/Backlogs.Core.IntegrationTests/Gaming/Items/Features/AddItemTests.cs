using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;
using BacklogOrganizer.Modules.Backlogs.Core.Models;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Items.Features;

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
        var request = new AddBacklogItemCommand(Backlog.InstanceId, expectedName);

        var result = await _factory.SendAsync(request);
        result.Should().BeSuccessful();

        var createdItem = await _factory.FindAsync<BacklogItem>(result.Value!.Id);
        createdItem.Should().NotBeNull();
        createdItem!.Name.Should().Be(expectedName);
        createdItem.Id.Should().NotBe(default(Guid));
        createdItem.CompletionStatusDetails.Status.Should().Be(ItemCompletionStatus.ToDo);
    }
}
