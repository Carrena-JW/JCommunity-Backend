namespace JCommunity.Web.Host.ApiEndpoints.Topic;

internal static class TopicCommandEndpoints
{
    private static readonly string API_ROOT = "/api/v1/Topics";
    private static readonly string[] API_TAG = { "#04. Topic Command API" };

    internal static IEndpointRouteBuilder ConfigureTopicCommandEndpoints(this RouteGroupBuilder app)
    {
        app.MapGroup(API_ROOT)
           .WithTags(API_TAG)
           .MapTopicCommandEndpoints();

        return app;
    }

    private static IEndpointRouteBuilder MapTopicCommandEndpoints(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapPost("/", CreateTopicAsync);
        app.MapPut("/", UpdateTopicAsync);
        app.MapDelete("/{id}", DeleteTopicAsync);

        app.MapPost("/{topicId}/TopicTags", AddTopicTagAsync);
        app.MapDelete("/{topicId}/TopicTags/{topicTagId}", DeleteTopicTagAsync);
        #endregion

        return app;
    }

    private static async Task<IResult> CreateTopicAsync(
        [FromBody] CreateTopic.Command request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }
    private static async Task<IResult> DeleteTopicAsync(
        [AsParameters] DeleteTopic.Command request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }


    private static async Task<IResult> UpdateTopicAsync(
        [FromBody] UpdateTopic.Command request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

    private static async Task<IResult> AddTopicTagAsync(
        [FromBody] AddTopicTag.Command request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }


    private static async Task<IResult> DeleteTopicTagAsync(
        [AsParameters] DeleteTopicTag.Command request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }


}

