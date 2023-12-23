namespace JCommunity.AppCore.Entities.TopicAggregate;
public class Topic : AggregateRoot 
{
    
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public HashSet<TopicTag> Tags { get; private set; } = new();
    public int Sort { get; private set; }
    public Member Author { get; private set; } = null!;
    [JsonIgnore]
    public Ulid AuthorId { get; private set; }
    
    public static Topic Create(
        string name,
        string description,
        int sort,
        Ulid authorId)
    {
        return new()
        {
            Id = Ulid.NewUlid(),
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

    public void UpdateTags(IEnumerable<string> tagNames)
    {
        var topicTags = tagNames.Select(t => TopicTag.Create(t));
        this.Tags = topicTags.ToHashSet();
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

    public IEnumerable<TopicTag> AddTags(IEnumerable<string> names)
    {
        var exceptName = names.Except(Tags.Select(t => t.Name)).ToList();
        var tags = exceptName.Select(n => TopicTag.Create(n)).ToList();

        foreach (var tag in tags)
        {
            Tags.Add(tag);
        }

        return tags;

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
 