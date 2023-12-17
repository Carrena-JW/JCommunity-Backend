namespace JCommunity.AppCore.Entities.Post;

public class PostContent : EntityBase
{
    public Guid PostId { get; internal set; }
    public string ThumbnailUrl { get; internal set; } = string.Empty;
    public string MainImageUrl { get; internal set; } = string.Empty;
    public HashSet<PostContentAttachment> Attachments { get; internal set; } = new();
    public string HtmlBody { get; internal set; } = string.Empty;
}
