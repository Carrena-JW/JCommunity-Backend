using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostComment : EntityBase
{
    public Guid PostId { get; private set; }
    public Guid? ParentCommentId { get; private set; } = null;
    public string Contents { get; private set; } = string.Empty;
    public Guid AuthorId { get; private set; }
    public Member Author { get; private set; } = null!;
    public HashSet<PostCommentLike> Likes { get; private set; } = new();
    public DateTime CreatedOrUpdatedAt { get; protected set; } = SystemTime.now();

    public static PostComment Create(
        string contents,
        Guid authorId,
        Guid? parentCommentId = null)
    {
        return new()
        {
            Contents = contents,
            AuthorId = authorId,
            ParentCommentId = parentCommentId
        };
    }

    public void UpdatePostCommentContents(string contents)
    {
        if(Contents != null)
        {
            this.Contents = contents;
            this.CreatedOrUpdatedAt = SystemTime.now();
        }
    }

    public PostCommentLike CreateUpdatePostCommentLike(Guid authorId,bool isLike)
    {
        var exists = this.Likes
            .SingleOrDefault(l => l.AuthorId == authorId);

        if(exists == null)
        {
            var postCommentLike = PostCommentLike.Create(
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
}
