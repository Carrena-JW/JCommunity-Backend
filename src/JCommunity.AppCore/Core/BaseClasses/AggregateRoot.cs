namespace JCommunity.AppCore.Core.BaseClasses;

public abstract class AggregateRoot : EntityBase, IAggregateRoot, IAuditEntity
{
    public HashSet<INotification> DomainEvents { get; private set; } = new();

    public void AddDomainEvent(INotification eventItem)
    {
        DomainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }

    public DateTime CreatedAt { get; protected set; } = SystemTime.now();

    public string CreatedMemberId { get; protected set; } = string.Empty;

    public DateTime LastUpdatedAt { get; protected set; } = SystemTime.now();

    public string LastUpdatedMemberId { get; protected set; } = string.Empty;
}

