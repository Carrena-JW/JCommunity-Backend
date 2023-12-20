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

    public async Task<Topic?> GetTopicByIdAsync(
         Guid topicId,
         TopicIncludeOptions? options = null,
         CancellationToken token = new())
    {
        var query = _appDbContext.Topics.AsQueryable();

        if (options != null) query = IncludeOption(options, query);

        return await query.SingleOrDefaultAsync(t => t.Id == topicId, token);
    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync(
        TopicIncludeOptions? options,
        CancellationToken token)
    {
        var query = _appDbContext.Topics.AsNoTracking();

        if (options != null) query = IncludeOption(options, query);

        return await query.ToListAsync(token);
    }


    public async Task<bool> IsUniqueTopicNameAsync(string name, CancellationToken token)
    {
        return !await _appDbContext.Topics.AnyAsync(t => t.Name == name, token);
    }

    private IQueryable<T> IncludeOption<T>(TopicIncludeOptions options, IQueryable<T> query) where T : Topic
    {
        if (options.IncludeAuthor.HasValue && options.IncludeAuthor.Value)
        {
            query = query.Include(t => t.Author);
        }

        if (options.IncludeTags.HasValue && options.IncludeTags.Value)
        {
            query = query.Include(t => t.Tags);
        }

        return query;
    }

    public async Task<bool> IsExistsTopicAsync(Guid topicId, CancellationToken token)
    {
        return await _appDbContext.Members.AsNoTracking()
            .AnyAsync(m => m.Id == topicId, token);
    }
}
