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

    public void UpdateMainImage(PostContentAttachment attachment)
    {
        var previousImage = Attachments.SingleOrDefault(a => a.Url == this.MainImageUrl);

        if(previousImage != null)
        {
            this.Attachments.Remove(previousImage);
        }

        AddAttachment(attachment);

        this.MainImageUrl = attachment.Url;
        var fileName = Path.GetFileName(attachment.Path);
        this.ThumbnailUrl = attachment.Url.Replace(fileName, $"thumb_{fileName}");
    }

    public void AddAttachment(PostContentAttachment attachment)
    {
        this.Attachments.Add(attachment);
    }

    public void UpdateHtmlBody(string htmlBody)
    {
        // #01. Conditioin for Length
        if (this.HtmlBody != htmlBody)
        {
            this.HtmlBody = htmlBody;
        }
    }
}
