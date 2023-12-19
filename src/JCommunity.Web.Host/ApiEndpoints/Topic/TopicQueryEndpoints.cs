namespace JCommunity.Web.Host.ApiEndpoints.Topic;

internal static class TopicQueryEndpoints
{
    private static readonly string API_ROOT = "/api/v1/Topics";
    private static readonly string[] API_TAG = { "#05. Topic Query API" };

    internal static IEndpointRouteBuilder ConfigureTopicQueryEndpoints(this RouteGroupBuilder app)
    {
        #region [Map Path]
        app.MapGroup(API_ROOT)
           .WithTags(API_TAG)
           .MapTopicQueryEndpoints();
        #endregion

        return app;
    }

    private static IEndpointRouteBuilder MapTopicQueryEndpoints(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapGet("/", GetTopicsAsync);
        app.MapGet("/{id}", GetTopicByIdAsync);
        app.MapGet("/{topicId}/TopicTags", GetTopicTagsByIdAsync);

        app.MapGet("/TopicTags", GetTopicTags);
        #endregion

        return app;
    }

    private static IResult GetTopicTags(CancellationToken token = new())
    {
        var tags = Enum.GetNames(typeof(Tag));

        return Results.Ok(new { Tags = tags } );
    }

    // include option
    private static async Task<IResult> GetTopicsAsync(
        [AsParameters] GetTopics.Query request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }
    private static async Task<IResult> GetTopicByIdAsync(
        [AsParameters] GetTopicById.Query request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        if (result.Value == null) return Results.Ok(new { });

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

    private static async Task<IResult> GetTopicTagsByIdAsync(
        [AsParameters] GetTopicTags.Query request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

}

