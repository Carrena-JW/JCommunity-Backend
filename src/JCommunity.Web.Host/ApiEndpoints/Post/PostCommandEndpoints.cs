
namespace JCommunity.Web.Host.ApiEndpoints.Post
{
    internal static class PostCommandEndpoints
    {
        private static readonly string API_ROOT = "/api/v1/Posts";
        private static readonly string[] API_TAG = { "#06. Post Command API" };

        internal static IEndpointRouteBuilder ConfigurePostCommandEndpoints(this RouteGroupBuilder app)
        {
            app.MapGroup(API_ROOT)
               .WithTags(API_TAG)
               .MapPostCommandEndpoints();

            return app;
        }

        internal static IEndpointRouteBuilder MapPostCommandEndpoints(this IEndpointRouteBuilder app)
        {
            #region [Map Path]
            app.MapPost("/", CreatePostAsync).DisableAntiforgery();
            app.MapPut("/", UpdatePostAsync).DisableAntiforgery();
            app.MapDelete("/{id}", DeletePostAsync);
            app.MapPost("/likes", SetPostLikeAsync);
            app.MapPost("/Comments", CreatePostCommentAsync);
            app.MapPut("/Comments", UpdatePostCommentAsync);
            app.MapDelete("/{postId}/Comments/{postCommentId}", DeletePostCommentAsync);
            app.MapPost("/Comments/Likes", SetPostCommentLikeAsync);
            app.MapPost("/Reports", CreatePostReportAsync);
            #endregion

            return app;
        }

       



        #region [Post Report]
        private static async Task<IResult> CreatePostReportAsync(
            [FromBody] CreatePostReport.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }
        #endregion

        #region [Post Like]
        private static async Task<IResult> SetPostLikeAsync(
            [FromBody] SetPostLike.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }
        #endregion

        #region [Post Comment]
        private static async Task<IResult> SetPostCommentLikeAsync(
            [FromBody] SetPostCommentLike.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> UpdatePostCommentAsync(
            [FromBody] UpdatePostComment.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> DeletePostCommentAsync(
            [AsParameters] DeletePostComment.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> CreatePostCommentAsync(
            [FromBody] CreatePostComment.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }
        #endregion

        #region [Post]
        private static async Task<IResult> UpdatePostAsync(
            IFormFile Image,
            [FromForm] UpdatePost.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            request = services.BindRequest(request);
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> DeletePostAsync(
            [AsParameters] DeletePost.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> CreatePostAsync(
            IFormFile Image,
            [FromForm] CreatePost.Command request,
            [AsParameters] PostApiService services,
            CancellationToken token = default)
        {
            if (Image == null) return Results.BadRequest("Images must be not null.");

            request = services.BindRequest(request);
            var result = await services.Mediator.Send(request, token);

            return result.IsSuccess ? Results.Ok(result.Value) :
                                      Results.BadRequest(result.Errors);
        }
        #endregion
    }

}
