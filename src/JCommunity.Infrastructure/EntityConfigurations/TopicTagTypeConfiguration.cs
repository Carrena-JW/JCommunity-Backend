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
         
        builder.HasIndex(b => b.Value).IsUnique();

        builder.HasIndex(b=> b.Name).IsUnique();
        builder.Property(b => b.Name)
            .HasMaxLength(TopicRestriction.CATEGORY_NAME_MAX_LENGTH);

    }
}

