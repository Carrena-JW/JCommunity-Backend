
namespace JCommunity.Web.Host.ApiEndpoints.File;

internal static class FileApi
{
    public static IEndpointRouteBuilder MapFileApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", UploadFileAsync);
        app.MapGet("/", DownloadFileAsync);
        return app;
    }

    private static async Task<IResult> UploadFileAsync(
        IFormFile file, 
        CancellationToken token = new())
    {
        
        return Results.Ok(Task.CompletedTask);
    }

    private static async Task<IResult> DownloadFileAsync(
        string fileId, 
        CancellationToken token = new())
    {
        return Results.Ok(Task.CompletedTask);
    }
}