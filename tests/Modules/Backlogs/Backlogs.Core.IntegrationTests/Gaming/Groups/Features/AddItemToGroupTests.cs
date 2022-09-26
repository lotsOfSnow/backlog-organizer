using System.Reflection;
using AutoFixture;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using Microsoft.EntityFrameworkCore;
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

        // TODO: Query to check if it worked.
        await AssertAssignments(backlog.Id, assignments =>
        {
            assignments.Should().HaveCount(items.Count);
            for (var i = 0; i < items.Count; i++)
            {
                var assignment = assignments.Single(x => x.ItemId == items[i].Id);
                assignment.GroupId.Should().Be(group.Id);
            }
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

        // TODO: Query to check if it worked.
        await AssertAssignments(emptyBacklog.Id, assignments
            => assignments.Should().BeEmpty());
    }

    public async Task AssertAssignments(Guid backlogId, Action<IReadOnlyList<GroupAssignment>> action)
    {
        // TODO: Just temporary until there's a query available.
        await Factory.ExecuteDbContextAsync(async (db) =>
        {
            var backlogAfterSaving = await db.Set<Backlog>().FirstAsync(x => x.Id == backlogId);
            var groups = (IEnumerable<BacklogGroup>)backlogAfterSaving.GetType().GetField("_groups", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(backlogAfterSaving);
            var group = groups.First();
            var assignments = ((IEnumerable<GroupAssignment>)group.GetType().GetField("_assignments", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(group)).ToList();
            action(assignments);
        });
    }
}
