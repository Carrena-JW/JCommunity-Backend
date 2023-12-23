namespace JCommunity.AppCore.Core.Abstractions;

public interface IDomainEvent : INotification
{
    Ulid EventId { get; }
    DateTime CreatedAt { get; }
   
}
