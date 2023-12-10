namespace JCommunity.Web.Host.SeedWork.ExceptionHandlers;
public class GlobalUnhandleExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalUnhandleExceptionHandler> _logger;

    public GlobalUnhandleExceptionHandler(ILogger<GlobalUnhandleExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken ct)
    {

        _logger.LogError(
           exception, "Exception occurred: {Message}", exception.Message);

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error.",
            Detail = exception.Message,
            Instance = exception.Source
        };

        httpContext.Response.StatusCode = details.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(details, ct);

        return true;

        
    }
}
