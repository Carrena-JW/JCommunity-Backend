namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostContentAttachmentTypeConfiguration 
    : IEntityTypeConfiguration<PostContentAttachment>
{
    public void Configure(EntityTypeBuilder<PostContentAttachment> builder)
    {
        builder.ToTable("post_content_attachments");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );

        builder.Property(builder => builder.PostContentId)
          .HasConversion(
              Id => Id.ToString(),
              value => Guid.Parse(value)
          );
 
    }
}
