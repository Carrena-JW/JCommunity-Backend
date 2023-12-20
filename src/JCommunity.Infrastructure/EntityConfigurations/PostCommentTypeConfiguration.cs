namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostCommentTypeConfiguration 
    : IEntityTypeConfiguration<PostComment>
{
    public void Configure(EntityTypeBuilder<PostComment> builder)
    {
        builder.ToTable("post_comments");

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

        builder.Property(builder => builder.AuthorId)
        .HasConversion(
            Id => Id.ToString(),
            value => Guid.Parse(value)
        );

        builder.Property(builder => builder.ParentCommentId)
        .HasConversion(
            Id => Id.ToString(),
            value => !string.IsNullOrEmpty(value) ? Guid.Parse(value) : null
        );

        // Contents
        builder.Property(builder => builder.Contents)
            .HasMaxLength(PostRestriction.COMMENT_CONTENTS_MAX_LENGTH);

        // Author (one to many)
        builder.HasOne(builder => builder.Author)
            .WithMany()
            .HasForeignKey(builder => builder.AuthorId);

        // Like (many to one)
        builder.HasMany(builder => builder.Likes)
           .WithOne()
           .HasForeignKey(builder => builder.CommentId);
    }
}
