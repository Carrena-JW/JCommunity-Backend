﻿namespace JCommunity.AppCore.Entities.Post;

public class PostReport : EntityBase
{
    public Guid PostId { get; private set; }
    public ReportCategory Category { get; private set; } = ReportCategory.Etc;
    public string Title { get; private set; } = string.Empty;
    public string Contents { get; private set; } = string.Empty;
    public DateTime CreatedOrUpdatedAt { get; private set; } = SystemTime.now();

    public Guid AuthorId { get; private set; }
    public Member.Member Author { get; private set; } = new();
}

public enum ReportCategory
{
    Sensational,
    Abusive,
    Disgust,
    Political,
    Etc
}