using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace PKMN.PokedexService.Api;

/// <summary>
/// This class handled exception globally, to hide error details in Api response.
/// </summary>
/// <param name="logger"></param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    /// <summary>
    /// Tries to handle the specified exception asynchronously within the ASP.NET Core
    /// pipeline. Implementations of this method can provide custom exception-handling
    /// logic for different scenarios
    /// </summary>
    /// <param name="httpContext">The HttpContext for the request.</param>
    /// <param name="exception">The unhandled exception</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous read operation. The value of its Result property contains
    /// the result of the handling operation. true if the exception was handled successfully; otherwise false.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, message: exception.Message);

        await httpContext.Response.WriteAsJsonAsync(new
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Internal Server Error"
        }, cancellationToken);
        return true;
    }
}

