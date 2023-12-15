namespace JCommunity.AppCore.Entities;
public class TopicIncludeOptions
{
    public bool? IncludeAuthor { get; init; }
    public bool? IncludeTags { get; init; }

    public static TopicIncludeOptions Create(bool? includeTags, bool? includeAuthor = false )
    {
        return new()
        {
            IncludeAuthor = includeAuthor,
            IncludeTags = includeTags
        };
    }
}

