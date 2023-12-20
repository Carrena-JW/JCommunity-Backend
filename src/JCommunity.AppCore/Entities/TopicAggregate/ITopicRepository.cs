namespace JCommunity.AppCore.Entities.TopicAggregate;

public interface ITopicRepository : IRepository<Topic>
{
    Topic Add(Topic topic);
    void Remove(Topic topic);

    Task<bool> IsExistsTopicAsync(Guid topicId, CancellationToken token);

    Task<Topic?> GetTopicByIdAsync(
        Guid topicId, 
        TopicIncludeOptions? options = null, 
        CancellationToken token = new());

    Task<IEnumerable<Topic>> GetAllTopicsAsync(
        TopicIncludeOptions? options = null,
        CancellationToken token = new());

    Task<bool> IsUniqueTopicNameAsync(string name, CancellationToken token = new());
 
}

