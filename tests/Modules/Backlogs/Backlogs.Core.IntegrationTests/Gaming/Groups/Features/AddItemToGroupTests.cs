using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.AddItems;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Core.Extensions;
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
    public async Task Can_add_item_to_group_if_item_exists_within_backlog()
    {
        var item = new GameBacklogItem("Item 1");
        var (backlog, group) = await SetupBacklogWithGroup((backlog) => backlog.AddItem(item));
        var expectedContents = new GameBacklogItem[] { item };

        var request = new AddItemsToGroupCommand(backlog.Id, group.Id, new[] { item.Id });

        var result = await _factory.SendAsync(request);

        var groupAfterCommand = await LoadGroupWithItems(group.Id);
        groupAfterCommand.Items.Should().BeEquivalentTo(expectedContents);
    }


    public async Task<(GamingBacklog Backlog, GameBacklogItemsGroup Group)> SetupBacklogWithGroup(Action<GamingBacklog> action)
    {
        var backlog = new GamingBacklog();
        var group = new GameBacklogItemsGroup("Test group");
        backlog.AddGroup(group);

        action(backlog);

        await _factory.ExecuteDbContextAsync(async db =>
        {
            db.GamingBacklogs.Add(backlog);
            await db.SaveChangesAsync();
        });

        return (backlog, group);
    }

    public async Task<GameBacklogItemsGroup> LoadGroupWithItems(Guid id)
    {
        var groupAfterCommand = default(GameBacklogItemsGroup);

        await _factory.ExecuteDbContextAsync(async db =>
        {
            groupAfterCommand = await db.Set<GameBacklogItemsGroup>()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
        });

        return groupAfterCommand ?? throw new InvalidOperationException($"Unable to find group with Id {id.InQuotationMarks()}");
    }
}
