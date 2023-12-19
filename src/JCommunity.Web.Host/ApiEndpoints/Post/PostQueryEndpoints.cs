namespace JCommunity.Web.Host.ApiEndpoints.Post;

internal static class PostQueryEndpoints
{
    private static readonly string API_ROOT = "/api/v1/Posts";
    private static readonly string[] API_TAG = { "#07. Post Query API" };

    internal static IEndpointRouteBuilder ConfigurePostQueryEndpoints(this RouteGroupBuilder app)
    {
        app.MapGroup(API_ROOT)
           .WithTags(API_TAG)
           .MapPostQueryEndpoints();

        return app;
    }
    private static IEndpointRouteBuilder MapPostQueryEndpoints(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapGet("/", GetPostsAsync);
        app.MapGet("/{id}", GetPostByIdAsync);

        app.MapGet("/{id}/Comments", GetPostCommentsAsync);
        app.MapGet("/{id}/Contents", GetPostContentsAsync);
        app.MapGet("/{id}/Likes", GetPostLikesAsync);
        app.MapGet("/{id}/Reports", GetPostReportsAsync);
        #endregion
        return app;
    }

    private static Task GetPostReportsAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task GetPostLikesAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task GetPostContentsAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static async Task<IResult> GetPostCommentsAsync(
        [AsParameters] GetPostComments.Query request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

    private static async Task<IResult> GetPostsAsync(
        [AsParameters] GetPosts.Query request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

    private static async Task<IResult> GetPostByIdAsync(
        [AsParameters] GetPostById.Query request,
        [AsParameters] TopicApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);

        if (result.Value == null) Results.Ok(new { });

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }
}