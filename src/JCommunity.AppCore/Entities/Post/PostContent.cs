namespace JCommunity.AppCore.Entities.Post;

public class PostContent : EntityBase
{
    public Guid PostId { get; private set; }
    public string ThumbnailUrl { get; private set; } = string.Empty;
    public string MainImageUrl { get; private set; } = string.Empty;
    public HashSet<PostContentAttachment> Attachments { get; private set; } = new();
    public string HtmlBody { get; private set; } = string.Empty;
}
