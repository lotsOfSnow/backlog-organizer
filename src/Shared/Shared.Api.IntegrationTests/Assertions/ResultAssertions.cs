using BacklogOrganizer.Shared.Core.Results;
using BacklogOrganizer.Shared.Core.Results.Errors;

namespace BacklogOrganizer.Shared.Api.IntegrationTests.Assertions;

public class ResultAssertions<T> :
    ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
{
    public ResultAssertions(Result<T> instance)
        : base(instance)
    {
    }

    public AndConstraint<ResultAssertions<T>> BeSuccessful(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsSuccess)
            .FailWith("Expected result to be successful, but it's failed");

        return new(this);
    }

    public AndConstraint<ResultAssertions<T>> BeFailed(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(!Subject.IsSuccess)
            .FailWith("Expected result to be failed, but it's successful");

        return new(this);
    }

    public AndConstraint<ResultAssertions<T>> HaveError(ErrorReason reason, string? errorText = null, string because = "", params object[] becauseArgs)
    {
        BeFailed(because, becauseArgs);

        if (Subject.Error is null)
        {
            Execute.Assertion
                .FailWith("Expected error to be set, but it's null");

            return new(this);
        }

        Execute.Assertion
            .ForCondition(Subject.Error.Reason == reason)
            .FailWith($"Expected reason of failure to be {reason}, but it's {Subject.Error.Reason}");

        if (errorText is not null)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Error.Text.Equals(errorText, StringComparison.Ordinal))
                .FailWith($"Expected error text to be '{errorText}', but it's '{Subject.Error.Text}'");
        }

        return new(this);
    }

    protected override string Identifier => "result";
}
