namespace JCommunity.Web.Host.SeedWork.Filters;

public class GlobalLoggingFilter : IEndpointFilter
{
    private readonly ILogger<GlobalLoggingFilter> _logger;

    public GlobalLoggingFilter(ILogger<GlobalLoggingFilter> logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var sb = new StringBuilder();
        var prefix = $"{context.HttpContext.GetEndpoint()}, {context.HttpContext.TraceIdentifier}";

        if (context.Arguments.Count > 0)
        {
            var requestParams = context.Arguments.OfType<IRequest>();

            foreach (var p in requestParams)
            {
                sb.Append(p.ToString());
            }
        }

        _logger.LogInformation($"Invoke endpoint Parameters : {prefix}, Arguments : {(sb.Length > 0 ? sb.ToString() : "NULL")}");

        return await next(context);
    }
}
