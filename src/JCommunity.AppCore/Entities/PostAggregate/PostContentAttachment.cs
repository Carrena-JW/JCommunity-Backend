namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostContentAttachment : EntityBase
{
    public Guid PostContentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public string FileExtention { get; private set; } = string.Empty;
    public long Size { get; private set; }
    public DateTime CreatedAt { get; private set; } = SystemTime.now();

    public static PostContentAttachment Create(
        string name,
        string path,
        string url,
        string fileExtention,
        long size)
    {
        return new()
        {
            Name = name,
            Path = path,
            Url = url,
            FileExtention = fileExtention,
            Size = size
        };
    }
}

 