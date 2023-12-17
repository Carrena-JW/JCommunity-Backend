namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostReportTypeConfiguration
    : IEntityTypeConfiguration<PostReport>
{
    public void Configure(EntityTypeBuilder<PostReport> builder)
    {
        builder.ToTable("post_reports");

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

        // Title
        builder.Property(builder => builder.Title)
            .HasMaxLength(PostRestriction.REPORT_TITLE_MAX_LENGTH);

        // Contents
        builder.Property(builder => builder.Contents)
            .HasMaxLength(PostRestriction.COMMENT_CONTENTS_MAX_LENGTH);

        // Author (one to many)
        builder.HasOne(builder => builder.Author)
            .WithMany()
            .HasForeignKey(builder => builder.AuthorId);

       
    }
}
