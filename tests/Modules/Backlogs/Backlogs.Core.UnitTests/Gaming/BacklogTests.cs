using AutoFixture;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
using BacklogOrganizer.Shared.Core.UnitTests.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Core.UnitTests.Gaming;
public class BacklogTests
{
    [Fact]
    public void Can_add_items_to_group_if_items_exist_within_backlog()
    {
        var fixture = new Fixture();
        var items = fixture.CreateMany<BacklogItem>().ToList();
        var (backlog, group) = GetBacklogWithGroupAndItems(items);

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
        var itemThatDoesNotExistInBacklog = new BacklogItem(Guid.NewGuid(), "Item 1");
        var (emptyBacklog, groupOfEmptyBacklog) = SetupBacklogWithGroup();

        emptyBacklog.AddItemsToGroup(groupOfEmptyBacklog.Id, new[] { itemThatDoesNotExistInBacklog.Id });

        emptyBacklog.AssertDomainEventNotPublished<NewGroupAssignmentCreatedDomainEvent>();
    }

    [Fact]
    public void Can_not_add_duplicate_items_to_backlog()
    {
        var item1 = new BacklogItem(Guid.NewGuid(), "Item1");
        var item2 = new BacklogItem(Guid.NewGuid(), "Item2");
        var item3 = new BacklogItem(item2.Id, "Item3");
        var backlog = new Backlog();

        var itemsWithDuplicatesToAddToBacklog = new[] { item1, item1, item2, item3 };
        foreach (var item in itemsWithDuplicatesToAddToBacklog)
        {
            backlog.AddItem(item);
        }

        var events = backlog.AssertPublishedDomainEvents<NewItemAddedDomainEvent>();
        events.Should().HaveCount(2);
        events.Should().OnlyContain(x => x.BacklogId == backlog.Id);
        events[0].ItemId.Should().Be(item1.Id);
        events[1].ItemId.Should().Be(item2.Id);
    }

    [Fact]
    public void Can_not_add_duplicate_items_to_backlog_group()
    {
        var item1 = new BacklogItem(Guid.NewGuid(), "Item1");
        var item2 = new BacklogItem(Guid.NewGuid(), "Item2");
        var item3 = new BacklogItem(item2.Id, "Item3");
        var uniqueBacklogItems = new[] { item1, item2 };
        var (backlog, group) = GetBacklogWithGroupAndItems(uniqueBacklogItems);

        var itemsWithDuplicatesToAddToGroup = new[] { item1, item1, item2, item3 };
        backlog.AddItemsToGroup(group.Id, itemsWithDuplicatesToAddToGroup.Select(x => x.Id));

        var events = backlog.AssertPublishedDomainEvents<NewGroupAssignmentCreatedDomainEvent>();
        events.Should().HaveCount(2);
        events.Should().OnlyContain(x => x.GroupId == group.Id);
        events[0].ItemId.Should().Be(item1.Id);
        events[1].ItemId.Should().Be(item2.Id);
    }

    private static (Backlog Backlog, BacklogGroup Group) GetBacklogWithGroupAndItems(IEnumerable<BacklogItem> items)
        => SetupBacklogWithGroup((backlog) =>
        {
            foreach (var item in items)
            {
                backlog.AddItem(item);
            }
        });


    private static (Backlog Backlog, BacklogGroup Group) SetupBacklogWithGroup(Action<Backlog>? action = null)
    {
        var backlog = new Backlog();
        var group = new BacklogGroup(Guid.NewGuid(), backlog.Id, "Test group");
        backlog.AddGroup(group);

        if (action is not null)
        {
            action(backlog);
        }

        return (backlog, group);
    }
}
