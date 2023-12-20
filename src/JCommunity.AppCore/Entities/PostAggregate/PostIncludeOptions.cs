namespace JCommunity.AppCore.Entities.PostAggregate;

public class PostIncludeOptions
{
    public bool? IncludeAuthor { get; private set; } = false;
    public bool? IncludeTopic { get; private set; } = false;
    public bool? IncludeLike { get; private set; } = false;
    public bool? IncludeComments { get; private set; } = false;
    public bool? IncludeReports { get; private set; } = false;
    public bool? IncludeContents { get; private set; } = false;

    public static PostIncludeOptions Build(
        bool? includeContents = false,
        bool? IncludeAuthor = false,
        bool? IncludeTopic = false,
        bool? IncludeLike = false,
        bool? IncludeComments = false,
        bool? IncludeReports = false
        )
    {
        return new()
        {
            IncludeContents = includeContents,
            IncludeAuthor = IncludeAuthor,
            IncludeTopic = IncludeTopic,
            IncludeLike = IncludeLike,
            IncludeComments = IncludeComments,
            IncludeReports = IncludeReports
        };
    }

    public PostIncludeOptions BindContentsOption(bool? value)
    {
        if (value == null) return this;

        this.IncludeContents = value;
        return this;
    }

    public PostIncludeOptions BindAuthorOption(bool? value)
    {
        if (value == null) return this;

        this.IncludeAuthor = value;
        return this;
    }

    public PostIncludeOptions BindTopicOption(bool? value)
    {
        if (value == null) return this;

        IncludeTopic = value;
        return this;
    }

    public PostIncludeOptions BindLikeOption(bool? value)
    {
        if (value == null) return this;

        IncludeLike = value;
        return this;
    }

    public PostIncludeOptions BindCommentsOption(bool? value)
    {
        if (value == null) return this;

        IncludeComments = value;
        return this;
    }

    public PostIncludeOptions BindReportsOption(bool? value)
    {
        if (value == null) return this;

        IncludeReports = value;
        return this;
    }
}
