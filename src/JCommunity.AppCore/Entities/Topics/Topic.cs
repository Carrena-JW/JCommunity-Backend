namespace JCommunity.AppCore.Entities.Topics;

public class Topic : IAuditEntity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<TopicTag> Tags { get; private set; } = new();
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

    public void AddTag(TopicTag tag)
    {
        if (Tags.Any(t => t.Id.Equals(tag.Id))) return;
        Tags.Add(tag);
    }

    public void AddTags(TopicTag[] tags)
    {
        Tags.AddRange(tags);
    }

    public void RemoveAllTags()
    {
        if(Tags.Count > 0) Tags.Clear();
    }

    public void RemoveTag(TopicTag tag)
    {
        Tags.Remove(tag);
    }
}

/*
    public const int NAME_MIN_LENGTH = 2;
    public const int NAME_MAX_LENGTH = 20;
    public const int DESCRIPTION_MAX_LENGTH = 100;
 */