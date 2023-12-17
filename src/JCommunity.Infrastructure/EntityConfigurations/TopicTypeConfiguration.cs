namespace JCommunity.Infrastructure.Entitybuilderurations;

internal class TopicTypeConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("topics");

        // Id
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );

        // Title
        builder.HasIndex(builder => builder.Name).IsUnique();
        builder.Property(builder => builder.Name)
            .HasMaxLength(TopicRestriction.NAME_MAX_LENGTH);

        // Description
        builder.Property(builder => builder.Description)
            .HasMaxLength(TopicRestriction.DESCRIPTION_MAX_LENGTH);

        // Sort
        builder.Property(builder => builder.Sort);


        // Tags 
        builder.HasMany(builder => builder.Tags)
             .WithOne()
             .HasForeignKey(builder => builder.TopicId);


        // Author
        builder.HasOne(builder => builder.Author)
            .WithMany()
            .HasForeignKey(builder => builder.AuthorId);



        builder.Property(builder => builder.AuthorId)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );


    }
}

