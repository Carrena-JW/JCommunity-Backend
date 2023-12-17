namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostContentAttachment : EntityBase
{
    public Guid PostContentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public string FileExtention { get; private set; } = string.Empty;
    public int Size { get; private set; }
    public DateTime CreatedAt { get; private set; } = SystemTime.now();
}
