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
}
