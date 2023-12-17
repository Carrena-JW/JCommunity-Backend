namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );

        builder.Property(builder => builder.Title)
            .HasMaxLength(PostRestriction.TITLE_MAX_LENGTH);

        // Topic (one to many)
        builder.Property(builder => builder.TopicId)
           .HasConversion(
               Id => Id.ToString(),
               value => Guid.Parse(value)
           );

        builder.HasOne(builder => builder.Topic)
            .WithMany()
            .HasForeignKey(builder => builder.TopicId);

        // PostContents (one to many)
        builder.HasOne(builder => builder.Contents)
            .WithOne()
            .HasForeignKey<PostContent>(builder => builder.PostId)
            .IsRequired();

        // Author (one to many)
        builder.Property(builder => builder.AuthorId)
           .HasConversion(
               Id => Id.ToString(),
               value => Guid.Parse(value)
           );

        builder.HasOne(builder => builder.Author)
            .WithMany()
            .HasForeignKey(builder => builder.AuthorId);


        // Like (many to one)
        builder.HasMany(builder => builder.Likes)
           .WithOne()
           .HasForeignKey(builder => builder.PostId);

        // Comments (many to one)
        builder.HasMany(builder => builder.Comments)
          .WithOne()
          .HasForeignKey(builder => builder.PostId);

        // Reports (many to one)
        builder.HasMany(builder => builder.Reports)
          .WithOne()
          .HasForeignKey(builder => builder.PostId);

      
    }
}
