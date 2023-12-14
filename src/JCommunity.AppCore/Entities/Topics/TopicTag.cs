namespace JCommunity.AppCore.Entities.Topics;

public class TopicTag
{
    public Guid Id { get; private set; }
    public Tag Value { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public List<Topic> Topics { get; private set; } = new();

    public static TopicTag Create(Tag tag)
    {
        return new() { 
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