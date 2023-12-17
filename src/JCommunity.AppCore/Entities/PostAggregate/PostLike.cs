namespace JCommunity.AppCore.Entities.PostAggregate
{
    public class PostLike : EntityBase
    {
        public Guid PostId { get; private set; }
        public Member Author { get; private set; } = null!;
        public Guid AuthorId { get; private set; }
        public bool IsLike { get; private set; }
    }
}
