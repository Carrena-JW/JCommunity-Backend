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

    public void Remove(Post post)
    {
        _appDbContext.Posts.Remove(post);
    }

    public async Task<Post?> GetPostByIdAsync(
        Guid postId,
        PostIncludeOptions? options = null,
        CancellationToken token = default)
    {
        var query = _appDbContext.Posts.AsQueryable();

        if(options != null) query = IncludeOption(options, query);

        return await query.SingleOrDefaultAsync(p => p.Id == postId, token);
        
    }

    public async Task<IEnumerable<Post>> GetPostsAsync(PostIncludeOptions? options,CancellationToken token)
    {
        var query = _appDbContext.Posts.AsQueryable();

        if (options != null)
        {
            query = IncludeOption(options, query);
        }

        return await query.ToListAsync(token);
    }

    private IQueryable<Post> IncludeOption(PostIncludeOptions options, IQueryable<Post> query) 
    {
        if(options.IncludeContents.HasValue && options.IncludeContents.Value)
        {
            query = query.Include(t => t.Contents)
                .ThenInclude(c => c.Attachments);

        }

        if (options.IncludeAuthor.HasValue && options.IncludeAuthor.Value)
        {
            query = query.Include(t => t.Author);
        }

        if (options.IncludeComments.HasValue && options.IncludeComments.Value)
        {
            query = query.Include(t => t.Comments)
                .ThenInclude(c => c.Likes);
            //query = query.Include(t => t.Comments.SelectMany(c => c.Likes));
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
