namespace JCommunity.AppCore.Core.Abstractions;

internal interface IAuditEntity
{
    DateTime CreatedAt { get; }
    string CreatedMemberId { get; }
    DateTime LastUpdatedAt { get; }
    string LastUpdatedMemberId { get; }

}
