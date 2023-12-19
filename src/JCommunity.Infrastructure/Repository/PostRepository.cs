namespace JCommunity.Infrastructure.Repository;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _appDbContext;

    public PostRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IUnitOfWork UnitOfWork => _appDbContext;

    public Post Add(Post topic)
    {
        return _appDbContext.Posts.Add(topic).Entity;
    }

    public async Task<Post?> GetPostById(
        Guid postId,
        PostIncludOptions? options = null,
        CancellationToken token = default)
    {
        var query = _appDbContext.Posts.AsQueryable();

        if(options != null) query = IncludeOption(options, query);

        return await query.SingleOrDefaultAsync(p => p.Id == postId, token);
        
    }

    public async Task<IEnumerable<Post>> GetPostsAsync(CancellationToken token)
    {
        return await _appDbContext.Posts.ToListAsync(token);
    }

    public async Task<IEnumerable<T>> GetPostsAsync<T>(CancellationToken token)
    {
        return await _appDbContext.Posts
            .Select(x => (T)Activator.CreateInstance(typeof(T))!).ToArrayAsync(token);                 
    }


    private IQueryable<Post> IncludeOption(PostIncludOptions options, IQueryable<Post> query) 
    {
        if (options.IncludeAuthor.HasValue && options.IncludeAuthor.Value)
        {
            query = query.Include(t => t.Author);
        }

        if (options.IncludeComments.HasValue && options.IncludeComments.Value)
        {
            query = query.Include(t => t.Comments);
        }

        if (options.IncludeLike.HasValue && options.IncludeLike.Value)
        {
            query = query.Include(t => t.Likes);
        }

        if (options.IncludeReports.HasValue && options.IncludeReports.Value)
        {
            query = query.Include(t => t.Reports);
        }

        if (options.IncludeTopic.HasValue && options.IncludeTopic.Value)
        {
            query = query.Include(t => t.Topic);
        }

        return query;
    }
}
