namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostContent : EntityBase
{
    public Guid PostId { get; internal set; }
    public string ThumbnailUrl { get; internal set; } = string.Empty;
    public string MainImageUrl { get; internal set; } = string.Empty;
    public HashSet<PostContentAttachment> Attachments { get; internal set; } = new();
    public string HtmlBody { get; internal set; } = string.Empty;

    public  static PostContent Create(
        string thumbnailUrl,
        string mainImageUrl,
        string htmlBody)
    {
        return new()
        {
            ThumbnailUrl = thumbnailUrl,
            MainImageUrl = mainImageUrl,
            HtmlBody = htmlBody
        };
    }

    public void AddAttachment(PostContentAttachment attachment)
    {
        this.Attachments.Add(attachment);
    }
}
