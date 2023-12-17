namespace JCommunity.AppCore.Entities.Post;

public class PostComment : EntityBase
{
    public Guid PostId { get; private set; }
    public Guid? ParentCommentId { get; private set; } = null;
    public string Contents { get; private set; } = string.Empty;
    public Guid AuthorId { get; private set; }
    public Member.Member Author { get; private set; } = null!;
    public DateTime CreatedOrUpdatedAt { get; protected set; } = SystemTime.now();


}
