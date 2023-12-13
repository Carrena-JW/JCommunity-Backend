namespace JCommunity.Infrastructure.Entitybuilderurations;

internal class TopicTagMapTypeConfiguration : IEntityTypeConfiguration<TopicTagMap>
{
    public void Configure(EntityTypeBuilder<TopicTagMap> builder)
    {
        builder.ToTable("topic_tag_map");
    }
}

