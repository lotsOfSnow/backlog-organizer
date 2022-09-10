using BacklogOrganizer.Shared.Core.Results;
using BacklogOrganizer.Shared.Core.Results.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BacklogOrganizer.Shared.Api.Controllers;

public static class ResultExtensions
{
    public static ObjectResult ToObjectResult<T>(this Result<T> result)
        where T : class
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        var error = result.Error;
        if (error is null)
        {
            throw new InvalidOperationException($"{nameof(result.IsSuccess)} is set to false, but there's no error set");
        }

        if (error.Reason is ErrorReason.ResourceNotFound)
        {
            return new NotFoundObjectResult(error.Text);
        }

        return new BadRequestObjectResult(error.Text);
    }
}
