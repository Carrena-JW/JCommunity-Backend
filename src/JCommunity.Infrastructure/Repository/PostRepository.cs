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

    public async Task<Post?> GetPostById(Guid postId, CancellationToken token)
    {
        return await _appDbContext.Posts.FindAsync(postId);
        
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
}
