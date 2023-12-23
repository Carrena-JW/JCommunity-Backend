namespace JCommunity.Infrastructure.Entitybuilderurations;

internal class TopicTagTypeConfiguration : IEntityTypeConfiguration<TopicTag>
{
    public void Configure(EntityTypeBuilder<TopicTag> builder)
    {
        builder.ToTable("topic_tags");
        
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion<UlidConverter>();


        builder.Property(builder => builder.Name)
            .HasMaxLength(TopicRestriction.TAG_NAME_MAX_LENGTH);

    }
}

