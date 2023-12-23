namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostReport : EntityBase
{
    public Ulid PostId { get; private set; }
    public ReportCategory Category { get; private set; } = ReportCategory.Etc;
    public string Title { get; private set; } = string.Empty;
    public string Contents { get; private set; } = string.Empty;
    public DateTime CreatedOrUpdatedAt { get; private set; } = SystemTime.now();
    public Ulid AuthorId { get; private set; }
    public Member Author { get; private set; } = null!;

    internal static PostReport Create(
        ReportCategory category,
        string title,
        string htmlbody,
        Ulid authorId)
    {
        return new()
        {
            Id = Ulid.NewUlid(),
            Category = category,
            Title = title,
            Contents = htmlbody,
            AuthorId = authorId
        };
    }
}

public enum ReportCategory
{
    Etc,
    Sensational,
    Abusive,
    Disgust,
    Political
}