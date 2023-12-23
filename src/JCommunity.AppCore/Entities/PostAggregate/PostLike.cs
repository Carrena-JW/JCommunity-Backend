namespace JCommunity.AppCore.Entities.PostAggregate
{
    public class PostLike : EntityBase
    {
        public Guid PostId { get; private set; }
        public Member Author { get; private set; } = null!;
        public Guid AuthorId { get; private set; }
        public bool IsLike { get; private set; }
        public DateTime CreatedOrUpdatedAt { get; protected set; } = SystemTime.now();

        internal static PostLike Create(Guid authorId,bool isLike)
        {
            return new()
            {
                AuthorId = authorId,
                IsLike = isLike
            };
        }

        internal void UpdateLike(bool value)
        {
            if (this.IsLike != value)
            {
                this.IsLike = value;
                this.CreatedOrUpdatedAt = SystemTime.now();
            }
        }
    }
}
