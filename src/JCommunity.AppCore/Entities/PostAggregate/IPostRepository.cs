namespace JCommunity.AppCore.Entities.PostAggregate;

public interface IPostRepository : IRepository<Post>
{
    Post Add(Post topic);
   

}

