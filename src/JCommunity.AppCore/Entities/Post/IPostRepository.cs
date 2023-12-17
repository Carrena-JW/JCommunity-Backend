namespace JCommunity.AppCore.Entities.Post;

public interface IPostRepository : IRepository<Post>
{
    Post Add(Post topic);
   

}

