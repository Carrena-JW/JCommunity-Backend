namespace JCommunity.IntergrationEvent;

public enum QueueRecordType
{
    None,
    PostCreated,
    PostUpdated,
    PostDeleted,
    TopicCreated,
    TopicUpdated,
    TopicDeleted,
    MemberCreated,
    MemberUpdated,
    MemberDeleted
}
