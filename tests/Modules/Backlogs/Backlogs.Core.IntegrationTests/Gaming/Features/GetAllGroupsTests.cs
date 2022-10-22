using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.GetAllGroups;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Features;
public class GetAllGroupsTests : BacklogTests
{
    public GetAllGroupsTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory) : base(testOutputHelper, factory)
    {
    }

    [Fact]
    public async Task Gets_all_groups()
    {
        var (backlog, group) = await SetupBacklogWithGroup();
        var query = new GetAllGroupsQuery(backlog.Id);

        var result = await Factory.SendAsync(query);

        result.Should().BeSuccessful();
        result.Value.Should().HaveCount(1);
        var groupDto = result.Value!.Single();
        groupDto.Id.Should().Be(group.Id);
        groupDto.BacklogId.Should().Be(group.BacklogId);
        groupDto.Name.Should().Be(group.Name);
    }

    [Fact]
    public async Task Returns_empty_successful_response_if_no_groups_exist()
    {
        var backlog = new Backlog(Guid.NewGuid());

        await Factory.ExecuteDbContextAsync(async db =>
        {
            db.Backlogs.Add(backlog);
            await db.SaveChangesAsync();
        });

        var result = await Factory.SendAsync(new GetAllGroupsQuery(backlog.Id));

        result.Should().BeSuccessful();
        result.Value.Should().BeEmpty();
    }
}
