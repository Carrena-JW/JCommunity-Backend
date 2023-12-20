namespace JCommunity.Web.Host.ApiEndpoints.Post;

internal class PostApiService(
    HttpContext httpContext,
    ILogger<PostApiService> logger,
    ISender mediator)
{
    public HttpContext HttpContext { get; } = httpContext;
    public ILogger<PostApiService> Logger { get; } = logger;
    public ISender Mediator { get; } = mediator;

    public CreatePost.Command BindRequest(CreatePost.Command request)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var json = HttpContext.Request.Form.SingleOrDefault();
        var bindRequest = JsonSerializer.Deserialize<CreatePost.Command>(json.Value!, options);

        return bindRequest! with { Image = request.Image };
    }

    public UpdatePost.Command BindRequest(UpdatePost.Command request)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var json = HttpContext.Request.Form.SingleOrDefault();
        var bindRequest = JsonSerializer.Deserialize<UpdatePost.Command>(json.Value!, options);

        if(request.Image != null)
        {
            return bindRequest! with { Image = request.Image };
        }
        else
        {
            return bindRequest!;
        }

    }
}
