using System.Diagnostics.CodeAnalysis;
using BacklogOrganizer.Shared.Core.Results.Errors;

namespace BacklogOrganizer.Shared.Core.Results;

public sealed class Result<T>
{
    [MemberNotNullWhen(true, nameof(Result<T>.Value))]
    [MemberNotNullWhen(false, nameof(Result<T>.Error))]
    public bool IsSuccess { get; }

    public T? Value { get; }

    public ResultError? Error { get; }

    public static Result<T> Success(T value)
        => new(true, value, null);

    public static Result<T> Failure(ResultError error)
        => new(false, default, error);

    private Result(bool isSuccess, T? value, ResultError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
}
