namespace JCommunity.AppCore.Entities.Post;

public class Post : AggregateRoot
{
    public Guid TopicId { get; private set; }
    public Topic Topic { get; private set; } = null!;
    public bool IsDraft { get; private set; } = true;
    public string Title { get; private set; } = string.Empty;
    public bool HasAttachments { get; private set; } = false;
    public bool IsReported { get; private set; } = false;
    public string Sources { get; private set; } = string.Empty;
    public Guid PostContentsId { get; private set; }
    public PostContent Contents { get; private set; } = new();
    public HashSet<PostLike> Likes { get; private set; } = new();
    public List<PostComment> Comments { get; private set; } = new();
    public List<PostReport> Reports { get; private set; } = new();
    public Guid AuthorId { get; private set; }
    public Member.Member Author { get; private set; } = null!;
}
