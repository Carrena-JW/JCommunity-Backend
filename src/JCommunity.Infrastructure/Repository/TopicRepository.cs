namespace JCommunity.Infrastructure.Repository;

public class TopicRepository : ITopicRepository
{
    private readonly AppDbContext _appDbContext;
    public IUnitOfWork UnitOfWork => _appDbContext;

    public TopicRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Topic Add(Topic topic)
    {
        return _appDbContext.Topics.Add(topic).Entity;
    }

    public void Remove(Topic topic)
    {
        _appDbContext.Topics.Remove(topic);
    }

    public async Task<Topic?> GetByIdAsync(Guid topicId, CancellationToken token)
    {
        return await _appDbContext.Topics.FindAsync(topicId, token);
    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync(CancellationToken token)
    {
        return await _appDbContext.Topics.ToListAsync(token);
    }

    public async Task<bool> IsUniqueTopicNameAsync(string name, CancellationToken token)
    {
        return !await _appDbContext.Topics.AnyAsync(t => t.Name == name, token);
    }
}
