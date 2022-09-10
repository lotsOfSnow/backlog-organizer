using BacklogOrganizer.Shared.Core.Results.Errors;

namespace BacklogOrganizer.Shared.Core.Results;

public record Result<T>(bool IsSuccess, T? Value, ResultError? Error)
{
    public static Result<T> Success(T value)
        => new(true, value, null);

    public static Result<T> Failure(ResultError error)
        => new(false, default, error);
}
