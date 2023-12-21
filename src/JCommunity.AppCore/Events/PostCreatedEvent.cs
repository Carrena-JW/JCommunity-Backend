namespace JCommunity.AppCore.Events;

public class PostCreatedEvent(Guid postId) : EventBase
{
    public Guid PostId { get; init; } = postId;
     
}
