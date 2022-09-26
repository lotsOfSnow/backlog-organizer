using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Groups.Features;
public class GetAssignmentsTests : GroupsTests
{
    public GetAssignmentsTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
        : base(testOutputHelper, factory)
    {
    }

    [Fact]
    public async Task Queries_assignments()
    {
        const int itemsLength = 3;
        var items = Enumerable.Range(0, itemsLength).Select(x => new BacklogItem(Guid.NewGuid(), $"Item{x + 1}")).ToList();
        var (backlog, group) = await SetupBacklogWithGroup((backlog, group) =>
        {
            foreach (var item in items)
            {
                backlog.AddItem(item);
            }

            backlog.AddItemsToGroup(group.Id, items.Select(x => x.Id));
        });
        var expectedResults = items.ConvertAll(x => new { ItemId = x.Id });

        var result = await Factory.SendAsync(new GetAssignmentsQuery(backlog.Id, group.Id));

        result.Should().BeSuccessful();
        result.Value.Should().HaveCount(3);
        result.Value.Should().BeEquivalentTo(expectedResults);

    }
}
