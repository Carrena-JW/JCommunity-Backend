namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostReportTypeConfiguration
    : IEntityTypeConfiguration<PostReport>
{
    public void Configure(EntityTypeBuilder<PostReport> builder)
    {
        builder.ToTable("post_reports");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion<UlidConverter>();

        builder.Property(builder => builder.PostId)
            .HasConversion<UlidConverter>();

        builder.Property(builder => builder.AuthorId)
            .HasConversion<UlidConverter>();

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
