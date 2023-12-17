namespace JCommunity.AppCore.Entities.Post;

public class PostIncludOptions
{
    public bool? IncludeAuthor { get; init; } = false;
    public bool? IncludeTopic { get; init; } = false;
    public bool? IncludeLike { get; init; } = false;
    public bool? IncludeComments { get; init; } = false;
    public bool? IncludeReports { get; init; } = false;

    public static PostIncludOptions Create(
        bool? IncludeAuthor,
        bool? IncludeTopic,
        bool? IncludeLike,
        bool? IncludeComments,
        bool? IncludeReports
        )
    {
        return new()
        {
            IncludeAuthor = IncludeAuthor,
            IncludeTopic = IncludeTopic,
            IncludeLike = IncludeLike,
            IncludeComments = IncludeComments,
            IncludeReports = IncludeReports
        };
    }
}
