using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostContent : EntityBase
{
    public Ulid PostId { get; internal set; }
    public string ThumbnailUrl { get; internal set; } = string.Empty;
    public string MainImageUrl { get; internal set; } = string.Empty;
    public HashSet<PostContentAttachment> Attachments { get; internal set; } = new();
    public string HtmlBody { get; internal set; } = string.Empty;

    internal static PostContent Create(
        string thumbnailUrl,
        string mainImageUrl,
        string htmlBody)
    {
        return new()
        {
            Id = Ulid.NewUlid(),
            ThumbnailUrl = thumbnailUrl,
            MainImageUrl = mainImageUrl,
            HtmlBody = htmlBody
        };
    }

    internal void UpdateMainImage(
        string fileName, 
        string filePath,
        long fileLength,
        Uri baseUri)
    {
        var attachment = PostContentAttachment
        .Create(fileName, filePath, baseUri, fileLength);

        var previousImage = Attachments.SingleOrDefault(a => a.Url == this.MainImageUrl);

        if(previousImage != null)
        {
            this.Attachments.Remove(previousImage);
        }

        AddAttachment(attachment);

        this.MainImageUrl = attachment.Url;
        var savedFileName = Path.GetFileName(attachment.Path);
        this.ThumbnailUrl = attachment.Url.Replace(savedFileName, $"thumb_{fileName}");
    }

    internal void AddAttachment(PostContentAttachment attachment)
    {
        this.Attachments.Add(attachment);
    }

    internal void UpdateHtmlBody(string htmlBody)
    {
        // #01. Conditioin for Length
        if (this.HtmlBody != htmlBody)
        {
            this.HtmlBody = htmlBody;
        }
    }
}
