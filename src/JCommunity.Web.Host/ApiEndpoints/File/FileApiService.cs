using JCommunity.Services.FileService;

namespace JCommunity.Web.Host.ApiEndpoints.File;

public class FileApiService(IFileService fileService)
{
    public IFileService fileService { get; set; } = fileService;
}
