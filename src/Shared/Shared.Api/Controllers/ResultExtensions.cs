using BacklogOrganizer.Shared.Core.Results;
using BacklogOrganizer.Shared.Core.Results.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.Shared.Api.Controllers;

public static class ResultExtensions
{
    public static ObjectResult ToObjectResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? new OkObjectResult(result.Value) : result.ToErrorResult();
    }

    public static ActionResult ToNoContentResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? new NoContentResult() : result.ToErrorResult();
    }

    private static ObjectResult ToErrorResult<T>(this Result<T> result)
    {
        var error = result.Error;
        return error is null
            ? throw new InvalidOperationException($"{nameof(result.IsSuccess)} is set to false, but there's no error set")
            : error.Reason is ErrorReason.ResourceNotFound ? new NotFoundObjectResult(error.Text) : new BadRequestObjectResult(error.Text);
    }
}
