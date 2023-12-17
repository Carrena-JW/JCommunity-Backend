using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JCommunity.Infrastructure.Repository;

public class FileRepository
{
    private readonly ILogger<FileRepository> _logger;

    private readonly IConfiguration _configuration;

    public FileRepository(IConfiguration configuration, ILogger<FileRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    //public async Task<string> SaveFileAsync(IFormFile file)
    //{
    //    var path = Path.Combine("wwwroot", file.FileName);
    //    using (var stream = System.IO.File.Create(path))
    //    {
    //        await file.CopyToAsync(stream);
    //    }

    //    return path;
    //}
}
