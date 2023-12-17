﻿
using JCommunity.AppCore.Entities.Topics;

namespace JCommunity.Web.Host.ApiEndpoints.Topic;

internal static class TopicQueryApi
{
    internal static IEndpointRouteBuilder MapTopicQueryApi(this IEndpointRouteBuilder app)
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
