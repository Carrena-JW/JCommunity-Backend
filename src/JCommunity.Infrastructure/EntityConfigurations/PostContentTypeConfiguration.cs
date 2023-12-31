﻿namespace JCommunity.Infrastructure.Entitybuilderurations;

public class PostContentTypeConfiguration : IEntityTypeConfiguration<PostContent>
{
    public void Configure(EntityTypeBuilder<PostContent> builder)
    {
        builder.ToTable("post_contents");

        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion<UlidConverter>();

        // Attachments (many to one)
        builder.HasMany(builder => builder.Attachments)
            .WithOne()
            .HasForeignKey(builder => builder.PostContentId);
        
    }
}
