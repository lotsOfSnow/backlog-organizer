using System.Reflection;
using AutoFixture;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Api.IntegrationTests.Assertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Groups.Features;
public class AddItemToGroupTests : IClassFixture<BacklogsApplicationFactory>
{
    private readonly BacklogsApplicationFactory _factory;

    public AddItemToGroupTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
    {
        _factory = factory;
        _factory.TestOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Can_add_items_to_group_if_items_exist_within_backlog()
    {
        var fixture = new Fixture();
        var items = fixture.CreateMany<GameBacklogItem>().ToList();
        var (backlog, group) = await SetupBacklogWithGroup((backlog) =>
        {
            foreach (var item in items)
            {
                backlog.AddItem(item);
            }
        });
        var expectedContents = items;

        var request = new AddItemsToGroupCommand(backlog.Id, group.Id, items.Select(x => x.Id));
        var result = await _factory.SendAsync(request);

        result.Should().BeSuccessful();

        // TODO: Query to check if it worked.
        await AssertAssignments(backlog.Id, assignments =>
        {
            assignments.Should().HaveCount(3);
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
        var (emptyBacklog, groupOfEmptyBacklog) = await SetupBacklogWithGroup((_) => { });
        var nonexistentItemId = Guid.NewGuid();

        var request = new AddItemsToGroupCommand(emptyBacklog.Id, groupOfEmptyBacklog.Id, new[] { nonexistentItemId });
        var result = await _factory.SendAsync(request);
        result.Should().BeSuccessful();

        // TODO: Query to check if it worked.
        await AssertAssignments(emptyBacklog.Id, assignments =>
        {
            assignments.Should().BeEmpty();
        });
    }

    public async Task<(GamingBacklog Backlog, GameBacklogItemsGroup Group)> SetupBacklogWithGroup(Action<GamingBacklog> action)
    {
        var backlog = new GamingBacklog();
        var group = new GameBacklogItemsGroup(Guid.NewGuid(), backlog.Id, "Test groupOfEmptyBacklog");
        backlog.AddGroup(group);

        action(backlog);

        await _factory.ExecuteDbContextAsync(async db =>
        {
            db.GamingBacklogs.Add(backlog);
            await db.SaveChangesAsync();
        });

        return (backlog, group);
    }

    public async Task AssertAssignments(Guid backlogId, Action<IReadOnlyList<GroupAssignment>> action)
    {
        // TODO: Just temporary until there's a query available.
        await _factory.ExecuteDbContextAsync(async (db) =>
        {
            var backlogAfterSaving = await db.Set<GamingBacklog>().FirstAsync(x => x.Id == backlogId);
            var groups = (IEnumerable<GameBacklogItemsGroup>)backlogAfterSaving.GetType().GetField("_groups", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(backlogAfterSaving);
            var group = groups.First();
            var assignments = ((IEnumerable<GroupAssignment>)group.GetType().GetField("_assignments", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(group)).ToList();
            action(assignments);
        });
    }
}
