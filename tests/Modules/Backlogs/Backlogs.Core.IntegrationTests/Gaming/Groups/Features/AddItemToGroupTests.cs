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

        await _factory.ExecuteDbContextAsync(async (db) =>
        {
            var b = await db.Set<GamingBacklog>().FirstAsync(x => x.Id == backlog.Id);
        });


        //groupAfterCommand.Items.Should().BeEquivalentTo(expectedContents);

        // TODO: Query to check if it worked.
    }

    [Fact]
    public async Task Can_not_add_item_to_group_if_item_does_not_exist_within_backlog()
    {
        var itemThatExistsInOtherBacklog = new GameBacklogItem("Item 1");
        var (backlogWithItem, _) = await SetupBacklogWithGroup((backlog) => backlog.AddItem(itemThatExistsInOtherBacklog));
        var (emptyBacklog, groupOfEmptyBacklog) = await SetupBacklogWithGroup((_) => { });

        var request = new AddItemsToGroupCommand(emptyBacklog.Id, groupOfEmptyBacklog.Id, new[] { itemThatExistsInOtherBacklog.Id });
        var result = await _factory.SendAsync(request);

        result.Should().BeSuccessful();
        //groupAfterCommand.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task Does_nothing_if_item_does_not_exist()
    {
        var (emptyBacklog, groupOfEmptyBacklog) = await SetupBacklogWithGroup((_) => { });
        var nonexistentItemId = Guid.NewGuid();

        var request = new AddItemsToGroupCommand(emptyBacklog.Id, groupOfEmptyBacklog.Id, new[] { nonexistentItemId });
        var result = await _factory.SendAsync(request);

        result.Should().BeSuccessful();
    }
    public async Task<(GamingBacklog Backlog, GameBacklogItemsGroup Group)> SetupBacklogWithGroup(Action<GamingBacklog> action)
    {
        var backlog = new GamingBacklog();
        // TODO: Incorrect, backlog has wrong ID
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
}
