namespace JCommunity.AppCore.Entities.PostAggregate;

public interface IPostRepository : IRepository<Post>
{
    Post Add(Post post);
    void Remove(Post post);
    Task<IEnumerable<Post>> GetPostsAsync(PostIncludeOptions? options, CancellationToken token);
    Task<Post?> GetPostByIdAsync(Guid postId, PostIncludeOptions? options, CancellationToken token);
}

