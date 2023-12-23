namespace JCommunity.AppCore.Entities.PostAggregate
{
    public class PostCommentLike : EntityBase
    {
        public Ulid CommentId { get; private set; }
        public Member Author { get; private set; } = null!;
        public Ulid AuthorId { get; private set; }
        public bool IsLike { get; private set; }
        public DateTime CreatedOrUpdatedAt { get; protected set; } = SystemTime.now();

        internal static PostCommentLike Create(
            Ulid authorId,
            bool isLike)
        {
            return new()
            {
                Id = Ulid.NewUlid(),
                AuthorId = authorId,
                IsLike = isLike
            };
        }

        internal void UpdateLike(bool value)
        {
            if(this.IsLike != value)
            {
                this.IsLike = value;
                this.CreatedOrUpdatedAt = SystemTime.now();
            }
        }
    }
}
