using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetAllItems;
using BacklogOrganizer.Shared.Api.IntegrationTests.Assertions;
using BacklogOrganizer.Shared.Core.Results.Errors;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Items.Features;

public class GetAllItemsTests : IClassFixture<BacklogsApplicationFactory>
{
    private readonly BacklogsApplicationFactory _factory;

    public GetAllItemsTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
    {
        _factory = factory;
        _factory.TestOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task Gets_all_items(int itemsToCreate)
    {
        var query = new GetAllItemsQuery(GamingBacklog.InstanceId);
        for (var i = 0; i < itemsToCreate; i++)
        {
            var creationCommand = new AddBacklogItemCommand(GamingBacklog.InstanceId, "Name");
            await _factory.SendAsync(creationCommand);
        }

        var response = await _factory.SendAsync(query);

        response.Should().BeSuccessful();
        response.Value.Should().HaveCount(itemsToCreate);
    }

    [Fact]
    public async Task Handles_wrong_backlog_id()
    {
        var nonExistingId = Guid.Parse("4de89043-5550-4ead-954d-d28488993f4d");
        var query = new GetAllItemsQuery(nonExistingId);

        var response = await _factory.SendAsync(query);

        response.Should().HaveError(ErrorReason.ResourceNotFound);
    }
}
