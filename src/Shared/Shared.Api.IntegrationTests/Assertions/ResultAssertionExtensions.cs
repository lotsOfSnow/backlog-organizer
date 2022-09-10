using BacklogOrganizer.Shared.Core.Results;

namespace BacklogOrganizer.Shared.Api.IntegrationTests.Assertions;

public static class ResultAssertionExtensions
{
    public static ResultAssertions<T> Should<T>(this Result<T> instance)
        => new(instance);
}
