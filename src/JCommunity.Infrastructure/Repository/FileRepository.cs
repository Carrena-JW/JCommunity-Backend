namespace JCommunity.Infrastructure.Repository;

public class FileRepository : IFileRepository
{
    private readonly string FILE_ROOT_PATH;
    private readonly int THUMBNAIL_WIDTH;
    private readonly ILogger<FileRepository> _logger;

    public FileRepository(IConfiguration configuration, ILogger<FileRepository> logger)
    {
        _logger = logger;
        FILE_ROOT_PATH = configuration.GetValue("File:FileUploadRootPath", string.Empty) ?? string.Empty;
        THUMBNAIL_WIDTH = configuration.GetValue("File:ThumbnailWidth", 20);
    }

    public async Task<(string FileType, Stream FileStream)> GetFileAsync(
        string fileName,
        CancellationToken token = default)
    {
        var path = Path.Combine(FILE_ROOT_PATH, fileName);
        var memory = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;


        return ("application/octet-stream", memory);

    }

    public async Task<string> SaveFileAsync(
        IFormFile file,
        bool containThumbnail = false,
        CancellationToken ct = default)
    {
        var guid = Guid.NewGuid().ToString().Replace("-", "");

        string extension = Path.GetExtension(file.FileName);

        var saveFileName = guid + extension;

        var path = Path.Combine(FILE_ROOT_PATH, saveFileName);

        using (var stream = File.Create(path))
        {
            await file.CopyToAsync(stream, ct);
        }

        #region [Create Thumbnail]
        var imageFormat = Image.DetectFormat(path);
        if (containThumbnail && imageFormat != null)
        {
            var ThumbnailPath = Path.Combine(FILE_ROOT_PATH, "thum_" + saveFileName);
            var image = Image.Load(path);
            image.Mutate(x => x.Resize(THUMBNAIL_WIDTH, 0));

            image.Save(ThumbnailPath);   
        }
        #endregion

        _logger.LogInformation("Uploading file - file: {@path}", path);
        return path;
    }
}



 
