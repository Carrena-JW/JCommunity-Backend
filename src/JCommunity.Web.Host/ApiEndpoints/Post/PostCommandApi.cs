namespace JCommunity.Web.Host.ApiEndpoints.Post
{
    internal static class PostCommandApi
    {
        public static IEndpointRouteBuilder MapPostCommandApi(this IEndpointRouteBuilder app)
        {
            #region [Map Path]
            app.MapPost("/", CreatePostAsync).DisableAntiforgery();

            #endregion

            return app;
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
