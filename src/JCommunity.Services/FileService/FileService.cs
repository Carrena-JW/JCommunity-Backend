namespace JCommunity.Services.FileService;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, bool containThumnail = false, CancellationToken token=default);
    Task<(string FileType, Stream FileStream)> GetFileAsync(string fileName, CancellationToken token = default);
}

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;

    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<string> SaveFileAsync(IFormFile file, bool containThumnail = false, CancellationToken token = default)
    {
        return await _fileRepository
            .SaveFileAsync(file, containThumnail, token);
    }

    public async Task<(string FileType, Stream FileStream)> GetFileAsync(string fileName, CancellationToken token = default)
    {
        return await _fileRepository.GetFileAsync(fileName,token);
    }
}
