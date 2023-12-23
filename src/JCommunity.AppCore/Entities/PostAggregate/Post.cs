using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JCommunity.AppCore.Entities.PostAggregate;

public class Post : AggregateRoot
{
    [JsonIgnore]
    public Guid TopicId { get; private set; }
    public Topic Topic { get; private set; } = null!;
    public bool IsDraft { get; private set; } = true;
    public string Title { get; private set; } = string.Empty;
    public bool HasAttachments { get; private set; } = false;
    public bool IsReported { get; private set; } = false;
    public string Sources { get; private set; } = string.Empty;
    public PostContent Contents { get; private set; } = null!;
    public HashSet<PostLike> Likes { get; private set; } = new();
    public List<PostComment> Comments { get; private set; } = new();
    public List<PostReport> Reports { get; private set; } = new();
    [JsonIgnore]
    public Guid AuthorId { get; private set; }
    public Member Author { get; private set; } = null!;

    public static Post Create(
        Guid topicId,
        string title,
        string htmlBody,
        string sources,
        Guid authorId,
        string fineName,
        string filePath,
        long fileLength,
        Uri baseUri
    )
    {
        var attachment = PostContentAttachment
        .Create(fineName, filePath, baseUri, fileLength);

        var fileName = Path.GetFileName(attachment.Path);
        var contents = PostContent
            .Create(attachment.Url.Replace(fileName, $"thumb_{fileName}"), attachment.Url, htmlBody);
        contents.AddAttachment(attachment);

        

        return new ()
        {
            TopicId = topicId,
            Title = title,
            Sources = sources,
            AuthorId = authorId,
            Contents = contents
        }; 
    }

    public void SetFinished()
    {
        this.IsDraft = false;
    }

    public void UpdateTopic(Guid topicId)
    {
        if(this.TopicId != topicId)
        {
            this.TopicId = topicId;
        }
    }

    public void UpdateTitle(string title)
    {
        if(this.Title != title)
        {
            this.Title = title;
        }
    }

    public void UpdateSources(string sources)
    {
        if(this.Sources != sources)
        {
            this.Sources = sources;
        }

    }

    public bool IsExistsComment(Guid postCommentId)
    {
        return this.Comments.Any(c => c.Id == postCommentId);
    }

    public PostComment AddComment(string contents, Guid authorId, Guid? parentCommentId)
    {
        var comment = PostComment.Create(contents, authorId, parentCommentId);

        this.Comments.Add(comment);

        return comment;
    }

    public void RemoveComment(Guid postCommentId)
    {
        var comment = this.Comments
            .SingleOrDefault(c => c.Id == postCommentId);

        if(comment != null)
        {
            this.Comments.Remove(comment);
        }
    }

    public PostComment? GetPostCommentById(Guid postCommentId)
    {
        return this.Comments.SingleOrDefault(c => c.Id == postCommentId);
    }

    public void UpdateLastUpdateAt()
    {
        this.LastUpdatedAt = SystemTime.now();
    }

    public PostLike CreateUpdatePostLike(Guid authorId, bool isLike)
    {
        var exists = this.Likes
            .SingleOrDefault(l => l.AuthorId == authorId);

        if (exists == null)
        {
            var postCommentLike = PostLike.Create(
                   authorId,
                   isLike);

            this.Likes.Add(postCommentLike);

            exists = postCommentLike;
        }
        else
        {
            exists.UpdateLike(isLike);
        }

        return exists;

    }

    public PostReport AddPostReport(
        int category, 
        string title, 
        string htmlBody, 
        Guid authorId)
    {
        

        var report = PostReport.Create((ReportCategory)category, title, htmlBody, authorId);
        this.Reports.Add(report);

        return report;
    }

    public bool IsDuplicatedReport(Guid authorId)
    {
        return this.Reports.Any(r => r.AuthorId == authorId);
    }

    public void UpdateMainImage(
        string fileName,
        string filePath,
        long fileLength,
        Uri baseUri)
    {
        this.Contents.UpdateMainImage(fileName, filePath, fileLength, baseUri);
    }

    public void UpdateHtmlBody(string htmlBody)
    {
        this.Contents.UpdateHtmlBody(htmlBody);
    }

    public PostCommentLike CreateUpdatePostCommentLike(
        PostComment postComment,
        Guid memberId,
        bool isLike)
    {
       return postComment.CreateUpdatePostCommentLike(memberId, isLike);
    }

    public void UpdatePostCommentContents(PostComment postComment,string contents) 
    { 
        postComment.UpdatePostCommentContents(contents);
    }
}
 