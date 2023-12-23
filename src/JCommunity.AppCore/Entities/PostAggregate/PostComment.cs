namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostComment : EntityBase
{
    public Ulid PostId { get; private set; }
    public Ulid? ParentCommentId { get; private set; } = null;
    public string Contents { get; private set; } = string.Empty;
    public Ulid AuthorId { get; private set; }
    public Member Author { get; private set; } = null!;
    public HashSet<PostCommentLike> Likes { get; private set; } = new();
    public DateTime CreatedOrUpdatedAt { get; protected set; } = SystemTime.now();

    internal static PostComment Create(
        string contents,
        Ulid authorId,
        Ulid? parentCommentId = null)
    {
        return new()
        {
            Id = Ulid.NewUlid(),
            Contents = contents,
            AuthorId = authorId,
            ParentCommentId = parentCommentId
        };
    }

    internal void UpdatePostCommentContents(string contents)
    {
        if(Contents != null)
        {
            this.Contents = contents;
            this.CreatedOrUpdatedAt = SystemTime.now();
        }
    }

    internal PostCommentLike CreateUpdatePostCommentLike(Ulid authorId,bool isLike)
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
