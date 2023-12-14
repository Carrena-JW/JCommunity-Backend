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

        app.MapPost("/{topicId}/topictags", CreateTopicTagAsync);
        app.MapDelete("/{topicId}/topictags/{topicTagId}", DeleteTopicTagAsync);
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

    private static Task CreateTopicTagAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task DeleteTopicTagAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task DeleteTopicAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task UpdateTopicAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

}

