namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostCommentLikeTypeConfiguration
    : IEntityTypeConfiguration<PostCommentLike>
{
    public void Configure(EntityTypeBuilder<PostCommentLike> builder)
    {
        builder.ToTable("post_comment_likes");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion<UlidConverter>();

        builder.Property(builder => builder.AuthorId)
            .HasConversion<UlidConverter>();
    }
}
