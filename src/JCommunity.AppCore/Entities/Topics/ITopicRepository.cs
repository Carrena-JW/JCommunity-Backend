namespace JCommunity.AppCore.Entities.Topics;

public interface ITopicRepository : IRepository<Topic>
{
    Topic Add(Topic topic);
    void Remove(Topic topic);

    Task<Topic?> GetByIdAsync(Guid topicId, CancellationToken token);
    Task<IEnumerable<Topic>> GetAllTopicsAsync(CancellationToken token);

    Task<bool> IsUniqueTopicNameAsync(string name, CancellationToken token);
}

