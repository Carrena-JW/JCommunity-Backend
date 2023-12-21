namespace JCommunity.AppCore.Core.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime CreatedAt { get; }
   
}
