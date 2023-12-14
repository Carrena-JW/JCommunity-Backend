namespace JCommunity.Infrastructure.Entitybuilderurations;

internal class TopicTagTypeConfiguration : IEntityTypeConfiguration<TopicTag>
{
    public void Configure(EntityTypeBuilder<TopicTag> builder)
    {

        
        builder.ToTable("topic_tags");
        
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
           .HasConversion(
               Id => Id.ToString(),
               value => Guid.Parse(value)
           );
         

        builder.Property(b => b.Name)
            .HasMaxLength(TopicRestriction.TAG_NAME_MAX_LENGTH);

    }
}

