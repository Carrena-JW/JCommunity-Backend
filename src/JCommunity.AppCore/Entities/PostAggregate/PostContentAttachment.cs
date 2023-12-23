namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostContentAttachment : EntityBase
{
    public Ulid PostContentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public string FileExtention { get; private set; } = string.Empty;
    public long Size { get; private set; }
    public DateTime CreatedAt { get; private set; } = SystemTime.now();

    internal static PostContentAttachment Create(
        string fileName,
        string filePath,
        Uri baseUri,
        long size)
    {
        var savedFileName = System.IO.Path.GetFileName(filePath);
        var downloadUri = new Uri(baseUri, savedFileName).ToString();
        var extention = System.IO.Path.GetExtension(fileName);

        return new()
        {
            Id = Ulid.NewUlid(),
            Name = fileName,
            Path = filePath,
            Url = downloadUri,
            FileExtention = extention,
            Size = size
        };
    }
}

 