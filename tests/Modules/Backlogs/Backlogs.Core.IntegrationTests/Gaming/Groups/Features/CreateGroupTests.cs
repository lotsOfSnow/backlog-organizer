using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups;
using BacklogOrganizer.Modules.Backlogs.Core.Gaming.Groups.Features.CreateGroup;
using BacklogOrganizer.Shared.Api.IntegrationTests.Assertions;
using Xunit.Abstractions;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests.Gaming.Groups.Features;
public class CreateGroupTests : IClassFixture<BacklogsApplicationFactory>
{
    private readonly BacklogsApplicationFactory _factory;

    public CreateGroupTests(ITestOutputHelper testOutputHelper, BacklogsApplicationFactory factory)
    {
        _factory = factory;
        _factory.TestOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Can_create_group()
    {
        const string expectedName = "Test group";

        var backlog = await _factory.GetNewBacklogAsync();
        var request = new CreateGroupCommand(backlog.Id, expectedName);

        var result = await _factory.SendAsync(request);

        result.Should().BeSuccessful();

        var createdGroup = await _factory.FindAsync<GameBacklogItemsGroup>(result.Value!.Id);
        createdGroup.Should().NotBeNull();
        createdGroup!.Name.Should().Be(expectedName);
        createdGroup.Id.Should().Be(result.Value.Id);
    }
}
