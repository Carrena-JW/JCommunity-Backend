namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostLikeTypeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.ToTable("post_likes");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );

        builder.Property(builder => builder.PostId)
         .HasConversion(
             Id => Id.ToString(),
             value => Guid.Parse(value)
         );
    }
}
