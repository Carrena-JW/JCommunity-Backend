namespace JCommunity.AppCore.Entities.PostAggregate;

public interface IPostRepository : IRepository<Post>
{
    Post Add(Post topic);


    Task<IEnumerable<Post>> GetPostsAsync(CancellationToken token);
    Task<Post?> GetPostById(Guid postId, PostIncludOptions? options, CancellationToken token);

       
}

