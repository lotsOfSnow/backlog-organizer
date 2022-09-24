using AutoFixture;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Core.UnitTests.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Core.UnitTests.Gaming;
public class GamingBacklogTests
{
    [Fact]
    public void Can_add_items_to_group_if_items_exist_within_backlog()
    {
        var fixture = new Fixture();
        var items = fixture.CreateMany<GameBacklogItem>().ToList();
        var (backlog, group) = SetupBacklogWithGroup((backlog) =>
        {
            foreach (var item in items)
            {
                backlog.AddItem(item);
            }
        });

        backlog.AddItemsToGroup(group.Id, items.Select(x => x.Id));

        var events = backlog.AssertPublishedDomainEvents<NewGroupAssignmentCreatedDomainEvent>();
        events.Should().HaveCount(items.Count);
        events.Should().OnlyContain(x => x.GroupId == group.Id);
        for (var i = 0; i < items.Count; i++)
        {
            events[i].ItemId.Should().Be(items[i].Id);
        }
    }

    [Fact]
    public void Can_not_add_item_to_group_if_item_does_not_exist_within_backlog()
    {
        var itemThatDoesNotExistInBacklog = new GameBacklogItem("Item 1");
        var (emptyBacklog, groupOfEmptyBacklog) = SetupBacklogWithGroup();

        emptyBacklog.AddItemsToGroup(groupOfEmptyBacklog.Id, new[] { itemThatDoesNotExistInBacklog.Id });

        emptyBacklog.AssertDomainEventNotPublished<NewGroupAssignmentCreatedDomainEvent>();
    }

    public static (GamingBacklog Backlog, GameBacklogItemsGroup Group) SetupBacklogWithGroup(Action<GamingBacklog>? action = null)
    {
        var backlog = new GamingBacklog();
        var group = new GameBacklogItemsGroup(Guid.NewGuid(), backlog.Id, "Test group");
        backlog.AddGroup(group);

        if (action is not null)
        {
            action(backlog);
        }

        return (backlog, group);
    }
}
