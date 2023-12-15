using JCommunity.Services.TopicService.Commands;

namespace JCommunity.Web.Host.ApiEndpoints.Topic;

public static class TopicCommandApi
{
    public static IEndpointRouteBuilder MapTopicCommandApi(this IEndpointRouteBuilder app)
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

