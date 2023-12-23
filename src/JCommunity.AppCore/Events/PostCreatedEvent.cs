namespace JCommunity.AppCore.Events;

public class PostCreatedEvent(Ulid postId) : EventBase
{
    public Ulid PostId { get; init; } = postId;
     
}
