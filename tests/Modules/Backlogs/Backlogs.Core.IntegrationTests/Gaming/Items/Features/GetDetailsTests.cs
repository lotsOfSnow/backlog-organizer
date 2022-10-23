using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.GetDetails;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Items.Features;
public class GetDetailsTests : BacklogTests
{
    public GetDetailsTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory) : base(testOutputHelper, factory)
    {
    }

    [Fact]
    public async Task Gets_details()
    {
        var item = new BacklogItem(Guid.NewGuid(), "New item");
        var (backlog, group) = await SetupBacklogWithGroup((backlog, group) =>
        {
            backlog.AddItem(item);
            backlog.AddItemsToGroup(group.Id, new[] { item.Id });
        });

        var query = new GetDetailsQuery(backlog.Id, item.Id);
        var response = await Factory.SendAsync(query);

        response.Should().BeSuccessful();
        response.Value!.Id.Should().Be(item.Id);
        response.Value.Name.Should().Be(item.Name);
        response.Value.GroupsAssigned.Should().BeEquivalentTo(new[] { group.Id });
    }
}
