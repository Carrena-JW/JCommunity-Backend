namespace JCommunity.AppCore.Entities.TopicAggregate;

public class TopicTag : EntityBase
{
    /* EF has a relationship mapping and retains its properties and without Dto,
     * infinite loop problems may occur during 
     * the Json direct deterioration process, so be sure to attach the tag
     * !!!!Must to add properties in child relationship properties!!!!!
     */
    [JsonIgnore]
    public Guid TopicId { get; private set; }
    public Tag Value { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public static TopicTag Create(Tag tag)
    {
        return new() {
            Id = Guid.NewGuid(),
            Name = Enum.GetName(tag)!, 
            Value = tag
        };
    }

    public static TopicTag Create(string name)
    {
        Tag tag;
        Enum.TryParse(name, out tag);

        return new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Value = tag
        };
    }
}

public enum Tag
{
    Unknown,
    Whatever,
    Politics,
    Economy,
    Sports,
    Game,
    Entertainment,
    Study,
    IT,
    Finance,
    Food,
    Humor,
    Hobby,
    Beauty
}