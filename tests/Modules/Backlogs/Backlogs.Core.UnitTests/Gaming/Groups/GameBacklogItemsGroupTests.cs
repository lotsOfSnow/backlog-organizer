using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;

namespace BacklogOrganizer.Modules.Backlogs.Core.UnitTests.Gaming.Groups;

public class GameBacklogItemsGroupTests
{
    [Fact]
    public void Does_not_add_duplicate_items()
    {
        var group = new GameBacklogItemsGroup("Group");

        var item = new GameBacklogItem("Item1")
        {
            Id = Guid.NewGuid()
        };

        var item2 = new GameBacklogItem("Item2")
        {
            Id = Guid.NewGuid()
        };

        var expectedItems = new GameBacklogItem[] { item, item2 };

        group.AddItems(item, item, item2);

        group.Items.Should().BeEquivalentTo(expectedItems);
    }
}
