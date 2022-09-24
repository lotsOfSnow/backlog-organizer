using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Events;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Shared.Core.UnitTests.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Core.UnitTests.Gaming.Groups;

public class GameBacklogItemsGroupTests
{
    [Fact]
    public void Does_not_add_duplicate_items()
    {
        var group = new GameBacklogItemsGroup(Guid.NewGuid(), Guid.NewGuid(), "Group");

        var item = new GameBacklogItem("Item1")
        {
            Id = Guid.NewGuid()
        };

        var item2 = new GameBacklogItem("Item2")
        {
            Id = Guid.NewGuid()
        };

        var item3 = new GameBacklogItem("Item3")
        {
            Id = item2.Id
        };

        var expectedItems = new GameBacklogItem[] { item, item2 };

        group.AddItems(item, item, item2, item3);

        var events = group.AssertPublishedDomainEvents<NewItemAddedDomainEvent>();
        events.Should().HaveCount(2);
        events.Should().OnlyContain(x => x.GroupId == group.Id);
        events[0].ItemId.Should().Be(item.Id);
        events[1].ItemId.Should().Be(item2.Id);
    }
}
