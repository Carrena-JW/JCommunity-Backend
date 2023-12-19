
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
            app.MapDelete("/{id}", DeletePostAsync);
            app.MapPut("/", UpdatePostAsync);

            app.MapPost("/{postId}/Comments", CreatePostCommentAsync);
            app.MapDelete("/{postId}/Comments/{postCommentId}", DeletePostCommentAsync);
            app.MapPut("/{postId}/Comments", UpdatePostCommentAsync);

            app.MapPost("/{postId}/Likes", CreatePostLikeAsync);
            app.MapDelete("/{postId}/Likes/{PostLikeId}", DeletePostLikeAsync);

            app.MapPost("/{postId}/Reports", CreatePostReportAsync);
            #endregion

            return app;
        }

        private static Task CreatePostReportAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task DeletePostLikeAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task CreatePostLikeAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task UpdatePostCommentAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task DeletePostCommentAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task CreatePostCommentAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task UpdatePostAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private static Task DeletePostAsync(HttpContext context)
        {
            throw new NotImplementedException();
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
    }
     
}
