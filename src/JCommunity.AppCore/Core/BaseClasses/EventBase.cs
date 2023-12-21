namespace JCommunity.AppCore.Core.BaseClasses;

public abstract class EventBase :IDomainEvent
{

    public Guid EventId { get; } = Guid.NewGuid();

    public DateTime CreatedAt { get; } = SystemTime.now();

}
