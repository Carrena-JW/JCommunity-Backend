namespace JCommunity.AppCore.Entities.Topics;

public class Topic : IAuditEntity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<TopicTag> Tags { get; private set; } = Array.Empty<TopicTag>();
    public int Sort { get; private set; }

    public Member.Member Author { get; private set; } = null!;
    public Guid AuthorId { get; private set; } 

    public DateTime CreatedAt { get; private set; } = SystemTime.now();
    public string CreatedMemberId { get; private set; } = string.Empty;
    public DateTime LastUpdatedAt { get; private set; } = SystemTime.now();
    public string LastUpdatedMemberId { get; private set; } = string.Empty;

    public static Topic Create(
        string name,
        string description,
        int sort,
        Guid authorId)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Sort = sort,
            AuthorId = authorId
            //Author = author
        };
    }
}

/*
    public const int NAME_MIN_LENGTH = 2;
    public const int NAME_MAX_LENGTH = 20;
    public const int DESCRIPTION_MAX_LENGTH = 100;
 */