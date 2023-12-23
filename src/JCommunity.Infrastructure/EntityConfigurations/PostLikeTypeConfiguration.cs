namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostLikeTypeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.ToTable("post_likes");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion<UlidConverter>();

        builder.Property(builder => builder.PostId)
            .HasConversion<UlidConverter>();

        builder.Property(builder => builder.AuthorId)
            .HasConversion<UlidConverter>();
    }
}
