namespace JCommunity.AppCore.Entities.Topics;

public class TopicTag
{
    public Guid Id { get; private set; }
    public Tag Value { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<Topic> Topics { get; private set; } = Array.Empty<Topic>();

    public static TopicTag Create(string name)
    {
        Tag result;
        Enum.TryParse(name, out result);

        return new() { 
            Id = Guid.NewGuid(), 
            Name = name, 
            Value = result
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