using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Groups;
public abstract class GroupsTests : IClassFixture<BacklogsApplicationFactory>
{
    protected BacklogsApplicationFactory Factory { get; }

    protected GroupsTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
    {
        Factory = factory;
        Factory.TestOutputHelper = testOutputHelper;
    }

    protected async Task<(Backlog Backlog, BacklogGroup Group)> SetupBacklogWithGroup(Action<Backlog, BacklogGroup>? action = null)
    {
        var backlog = new Backlog(Guid.NewGuid());
        var group = new BacklogGroup(Guid.NewGuid(), backlog.Id, "Test group");
        backlog.AddGroup(group);

        action?.Invoke(backlog, group);

        await Factory.ExecuteDbContextAsync(async db =>
        {
            db.Backlogs.Add(backlog);
            await db.SaveChangesAsync();
        });

        return (backlog, group);
    }
}
