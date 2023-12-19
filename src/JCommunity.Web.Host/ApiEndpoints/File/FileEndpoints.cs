namespace JCommunity.Web.Host.ApiEndpoints.File;

internal static class FileEndpoints
{
    private static readonly string API_ROOT = "/api/v1/file";
    private static readonly string[] API_TAG = { "#01. File API" };

    internal static IEndpointRouteBuilder ConfigureFileEndpoints(this RouteGroupBuilder app)
    {
        app.MapGroup(API_ROOT)
           .WithTags(API_TAG)
           .MapFileEndpoints();
       
        return app;
    }

    private static IEndpointRouteBuilder MapFileEndpoints(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapPost("/", UploadFileAsync).DisableAntiforgery();
        app.MapGet("/{fileName}", DownloadFileAsync);
        #endregion

        return app;
    }
   
    private static async Task<IResult> UploadFileAsync(
        IFormFile file,
        [FromQuery] bool containThumnail,
        [AsParameters] FileApiService services,
        CancellationToken token = default)
    {
        var result = await services.fileService.SaveFileAsync(file, containThumnail, token);

        return Results.Ok(result);
    }

    private static async Task<IResult> DownloadFileAsync(
        string fileName,
        [AsParameters] FileApiService services,
        CancellationToken token = new())
    {
        var (fileType, fileStream) = await services.fileService.GetFileAsync(fileName,token);
        return Results.File(fileStream, fileType);
    }
}