namespace JCommunity.AppCore.Core.BaseClasses;

public abstract class EventBase :IDomainEvent
{

    public Ulid EventId { get; } = Ulid.NewUlid();

    public DateTime CreatedAt { get; } = SystemTime.now();

}
