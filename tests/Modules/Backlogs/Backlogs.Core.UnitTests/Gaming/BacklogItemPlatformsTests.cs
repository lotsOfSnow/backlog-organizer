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
        var backlog = new Backlog(Guid.NewGuid());

        var uniquePlatforms = new[] { Platform.Default(Guid.NewGuid(), "Platform1"), Platform.Default(Guid.NewGuid(), "Platform2") };
        var platform3 = Platform.Default(uniquePlatforms[0].Id, "Platform3");
        var platformsToAdd = new[] { uniquePlatforms[0], uniquePlatforms[1], uniquePlatforms[1], platform3 };
        var item = new BacklogItem(Guid.NewGuid(), "Item", platformsToAdd);
        backlog.AddItem(item);

        var expectedPlatforms = uniquePlatforms;
        var events = backlog.AssertPublishedDomainEvents<NewPlatformAssignedDomainEvent>();
        events.Should().HaveSameCount(uniquePlatforms);
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
        backlog.UpdateItemPlatforms(item.Id, new[] { platform });

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
        backlog.UpdateItemPlatforms(item.Id, duplicatePlatforms);

        backlog.AssertDomainEventNotPublished<NewPlatformAssignedDomainEvent>();
    }

    [Fact]
    public void Can_remove_all_platforms()
    {
        var backlog = new Backlog(Guid.NewGuid());
        var platform = GetPlatform();
        var item = new BacklogItem(Guid.NewGuid(), "Item", new[] { platform });
        backlog.AddItem(item);

        backlog.UpdateItemPlatforms(item.Id, Array.Empty<Platform>());

        var events = backlog.AssertPublishedDomainEvents<PlatformUnassignedDomainEvent>();
        events.Should().ContainSingle();
        events.Single().ItemId.Should().Be(item.Id);
        events.Single().PlatformId.Should().Be(platform.Id);
    }

    [Fact]
    public void Can_selectively_update_platforms()
    {
        var backlog = new Backlog(Guid.NewGuid());
        var platformToKeep = GetPlatform();
        var platformToRemove = GetPlatform();
        var item = new BacklogItem(Guid.NewGuid(), "Item", new[] { platformToKeep, platformToRemove });
        backlog.AddItem(item);
        backlog.ClearAllDomainEvents();

        var newPlatforms = new[] { GetPlatform(), GetPlatform() };
        backlog.UpdateItemPlatforms(item.Id, new[] { platformToKeep, newPlatforms[0], newPlatforms[1] });

        var unassigningEvents = backlog.AssertPublishedDomainEvents<PlatformUnassignedDomainEvent>();
        unassigningEvents.Should().ContainSingle();
        unassigningEvents.Single().ItemId.Should().Be(item.Id);
        unassigningEvents.Single().PlatformId.Should().Be(platformToRemove.Id);

        var assigningEvents = backlog.AssertPublishedDomainEvents<NewPlatformAssignedDomainEvent>();
        assigningEvents.Should().HaveSameCount(newPlatforms);
        for (var i = 0; i < assigningEvents.Count; i++)
        {
            assigningEvents[i].ItemId.Should().Be(item.Id);
            assigningEvents[i].PlatformId.Should().Be(newPlatforms[i].Id);
        }
    }

    private static Platform GetPlatform()
        => Platform.Default(Guid.NewGuid(), "Default");

}
