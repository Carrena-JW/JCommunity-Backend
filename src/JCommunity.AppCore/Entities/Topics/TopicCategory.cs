namespace JCommunity.AppCore.Entities.Topics;

public class TopicCategory
{
    public Guid Id { get; private set; }
    public Category Value { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public static TopicCategory Create(string name)
    {
        Category result;
        Enum.TryParse(name, out result);

        return new() { 
            Id = Guid.NewGuid(), 
            Name = name, 
            Value = result
        };
    }
}

public enum Category
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