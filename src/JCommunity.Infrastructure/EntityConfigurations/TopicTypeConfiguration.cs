

using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

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


        // Category
        //builder.HasMany(builder => builder.Categories)
        //    .WithMany();
           


        // Author
        builder.HasOne<Member>()
            .WithOne()
            .HasPrincipalKey<Topic>(b=> b.AuthorId);

        builder.Property(b => b.AuthorId)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );

    
    }
}


internal class TopicCategoryTypeConfiguration : IEntityTypeConfiguration<TopicCategory>
{
    public void Configure(EntityTypeBuilder<TopicCategory> builder)
    {

        
        builder.ToTable("topic_categories");
        
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

