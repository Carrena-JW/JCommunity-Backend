namespace JCommunity.Web.Host.ApiEndpoints.Topic;

internal static class TopicQueryApi
{

    internal static IEndpointRouteBuilder MapTopicQueryApi(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapGet("/", GetTopicAsync);
        app.MapGet("/{id}", GetTopicByIdAsync);
        app.MapGet("/{topicId}/topictags", GetTopicTagsByIdAsync);
        #endregion

        return app;
    }

    private static Task GetTopicTagsByIdAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task GetTopicByIdAsync()
    {
        throw new NotImplementedException();
    }

    private static Task GetTopicAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}

