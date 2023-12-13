using JCommunity.AppCore.Entities.Member;

namespace JCommunity.AppCore.Entities.Topics;

public class Topic : IAuditEntity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    //public ICollection<TopicCategory> Categories { get; private set; } = Array.Empty<TopicCategory>();
    public int Sort { get; private set; }
    public Guid AuthorId { get; private set; } 

    public DateTime CreatedAt { get; private set; } = SystemTime.now();
    public string CreatedMemberId { get; private set; } = string.Empty;
    public DateTime LastUpdatedAt { get; private set; } = SystemTime.now();
    public string LastUpdatedMemberId { get; private set; } = string.Empty;


}

/*
    public const int NAME_MIN_LENGTH = 2;
    public const int NAME_MAX_LENGTH = 20;
    public const int DESCRIPTION_MAX_LENGTH = 100;
 */