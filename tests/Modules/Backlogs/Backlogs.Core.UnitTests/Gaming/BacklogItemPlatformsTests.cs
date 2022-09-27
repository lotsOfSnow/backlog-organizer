using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Events;
using BacklogOrganizer.Shared.Core.UnitTests.Extensions;

namespace BacklogOrganizer.Modules.Backlogs.Core.UnitTests.Gaming;
public class BacklogItemPlatformsTests
{
    [Fact]
    public void Assigns_initial_unique_platforms()
    {
        const int platformsToCreate = 2;
        var platform1 = Platform.Default(Guid.NewGuid(), "Platform1");
        var platform2 = Platform.Default(Guid.NewGuid(), "Platform2");
        var platform3 = Platform.Default(platform1.Id, "Platform3");

        var platformsToAdd = new[] { platform1, platform2, platform2, platform3 };
        var backlog = new Backlog(Guid.NewGuid());

        var item = new BacklogItem(Guid.NewGuid(), "Item", platformsToAdd);
        backlog.AddItem(item);

        var expectedPlatforms = new[] { platform1, platform2 };
        var events = backlog.AssertPublishedDomainEvents<NewPlatformAssignedDomainEvent>();
        events.Should().HaveCount(platformsToCreate);
        for (var i = 0; i < events.Count; i++)
        {
            events[i].ItemId.Should().Be(item.Id);
            events[i].PlatformId.Should().Be(expectedPlatforms[i].Id);
        }
    }

    [Fact]
    public void Can_add_platform()
    {
        var backlog = new Backlog(Guid.NewGuid());
        var item = new BacklogItem(Guid.NewGuid(), "Item");
        backlog.AddItem(item);

        var platform = GetPlatform();
        backlog.AddItemPlatform(item.Id, platform);

        var events = backlog.AssertPublishedDomainEvents<NewPlatformAssignedDomainEvent>();
        events.Should().ContainSingle();
        events.Single().ItemId.Should().Be(item.Id);
        events.Single().PlatformId.Should().Be(platform.Id);
    }

    [Fact]
    public void Can_not_add_duplicate_platform()
    {
        var backlog = new Backlog(Guid.NewGuid());
        var platform = GetPlatform();
        var item = new BacklogItem(Guid.NewGuid(), "Item", new[] { platform });
        backlog.AddItem(item);
        backlog.ClearAllDomainEvents();

        var duplicatePlatforms = new[] { platform, Platform.Default(platform.Id, "New name") };
        foreach (var duplicatePlatform in duplicatePlatforms)
        {
            backlog.AddItemPlatform(item.Id, duplicatePlatform);
        }

        backlog.AssertDomainEventNotPublished<NewPlatformAssignedDomainEvent>();
    }

    [Fact]
    public void Can_remove_platform()
    {
        var backlog = new Backlog(Guid.NewGuid());
        var platform = GetPlatform();
        var item = new BacklogItem(Guid.NewGuid(), "Item", new[] { platform });
        backlog.AddItem(item);

        backlog.RemoveItemPlatform(item.Id, platform.Id);

        var events = backlog.AssertPublishedDomainEvents<PlatformUnassignedDomainEvent>();
        events.Should().ContainSingle();
        events.Single().ItemId.Should().Be(item.Id);
        events.Single().PlatformId.Should().Be(platform.Id);
    }

    private static Platform GetPlatform()
        => Platform.Default(Guid.NewGuid(), "Default");

}
