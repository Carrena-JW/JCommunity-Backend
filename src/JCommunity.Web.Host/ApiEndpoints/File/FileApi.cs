namespace JCommunity.Web.Host.ApiEndpoints.File;

internal static class FileApi
{
    public static IEndpointRouteBuilder MapFileApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", UploadFileAsync).DisableAntiforgery();
        app.MapGet("/{fileName}", DownloadFileAsync);
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