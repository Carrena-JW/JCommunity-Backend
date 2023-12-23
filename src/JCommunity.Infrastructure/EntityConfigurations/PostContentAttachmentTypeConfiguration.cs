namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostContentAttachmentTypeConfiguration 
    : IEntityTypeConfiguration<PostContentAttachment>
{
    public void Configure(EntityTypeBuilder<PostContentAttachment> builder)
    {
        builder.ToTable("post_content_attachments");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion<UlidConverter>();

        builder.Property(builder => builder.PostContentId)
            .HasConversion<UlidConverter>();

    }
}
