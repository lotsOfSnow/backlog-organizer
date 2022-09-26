using AutoFixture;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.GetItems;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Groups.Features;
public class AddItemToGroupTests : GroupsTests
{
    public AddItemToGroupTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
        : base(testOutputHelper, factory)
    {
    }

    [Fact]
    public async Task Can_add_items_to_group_if_items_exist_within_backlog()
    {
        var fixture = new Fixture();
        var items = fixture.CreateMany<BacklogItem>().ToList();
        var (backlog, group) = await SetupBacklogWithGroup((backlog, _) =>
        {
            foreach (var item in items)
            {
                backlog.AddItem(item);
            }
        });
        var expectedContents = items;

        var request = new AddItemsToGroupCommand(backlog.Id, group.Id, items.Select(x => x.Id));
        var result = await Factory.SendAsync(request);

        result.Should().BeSuccessful();

        await AssertAssignments(backlog.Id, group.Id, assignments =>
        {
            assignments.Should().HaveCount(items.Count);
            var expected = items.Select(x => new { ItemId = x.Id });
            assignments.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public async Task Does_nothing_if_item_does_not_exist()
    {
        var (emptyBacklog, groupOfEmptyBacklog) = await SetupBacklogWithGroup();
        var nonexistentItemId = Guid.NewGuid();

        var request = new AddItemsToGroupCommand(emptyBacklog.Id, groupOfEmptyBacklog.Id, new[] { nonexistentItemId });
        var result = await Factory.SendAsync(request);
        result.Should().BeSuccessful();

        await AssertAssignments(emptyBacklog.Id, groupOfEmptyBacklog.Id, assignments
            => assignments.Should().BeEmpty());
    }

    public async Task AssertAssignments(Guid backlogId, Guid groupId, Action<IEnumerable<GroupAssignmentDto>> action)
    {
        var assignments = await Factory.SendAsync(new GetAssignmentsQuery(backlogId, groupId));
        action(assignments.Value!);
    }
}
