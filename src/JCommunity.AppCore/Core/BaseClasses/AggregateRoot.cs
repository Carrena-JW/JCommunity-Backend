namespace JCommunity.AppCore.Core.BaseClasses;

public class ArregateRoot : EntityBase, IAggregateRoot, IAuditEntity
{
    public DateTime CreatedAt { get; protected set; } = SystemTime.now();

    public string CreatedMemberId { get; protected set; } = string.Empty;

    public DateTime LastUpdatedAt { get; protected set; } = SystemTime.now();

    public string LastUpdatedMemberId { get; protected set; } = string.Empty;
}

