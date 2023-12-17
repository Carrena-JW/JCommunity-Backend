namespace JCommunity.AppCore.Entities.Topics;

public class Topic : AggregateRoot 
{
    
    public string Name { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public HashSet<TopicTag> Tags { get; private set; } = new();
    public int Sort { get; private set; }
    public Member.Member Author { get; private set; } = null!;
    [JsonIgnore]
    public Guid AuthorId { get; private set; }
    
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
        };
    }

    /*
    * Update Target:
    *  - Name
    *  - Description
    *  - Tags
    *  - Sort
    */

    public void UpdateTopicName(string name)
    {
        if (Name != name) Name = name;
    }

    public void UpdateTopicDescription(string desc)
    {
        if (Description != desc) Description = desc;
    }

    public void UpdateTopicSortOrder(int sort)
    {
        if (Sort != sort) Sort = sort;
    }

    public void UpdateTags(IEnumerable<TopicTag> tags)
    {
        this.Tags = tags.ToHashSet();
    }

    public void AddTag(TopicTag tag)
    {
        if (Tags.Any(t => t.Id.Equals(tag.Id))) return;
        Tags.Add(tag);
    }

    public void AddTags(TopicTag[] tags)
    {
        

        foreach (var tag in tags.AsEnumerable())
        {
            Tags.Add(tag);
        }
         
    }

    public void RemoveAllTags()
    {
        if(Tags.Count > 0) Tags.Clear();
    }

    public void RemoveTag(TopicTag tag)
    {
        Tags.Remove(tag);
    }

    public void UpdateLastUpdateAt()
    {
        this.LastUpdatedAt = SystemTime.now();
    }
}
 