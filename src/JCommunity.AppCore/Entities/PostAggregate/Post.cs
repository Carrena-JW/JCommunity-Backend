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
    public PostContent Contents { get; private set; } = new();
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
        Guid authorId
        )
    {
        var draft = new Post
        {
            TopicId = topicId,
            Title = title,
            Sources = sources,
            AuthorId = authorId
        };

        draft.Contents.HtmlBody = htmlBody;

        return draft;
    }
}
