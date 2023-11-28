namespace JComunity.AppCore.Abstractions;

internal interface IAuditEntity
{
    DateTime CreatedAt { get; } 
    MemberId CreatedMemberId { get; }
    DateTime LastUpdatedAt { get; }
    MemberId LastUpdatedMemberId { get; }

}
