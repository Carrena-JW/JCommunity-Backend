using Microsoft.Extensions.DependencyInjection;

namespace JCommunity.Infrastructure.Setup;

public class SetupTopicTagSeed 
{
    public static void Setup(IServiceProvider serviceProvider)
    {

        using var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        // Setup Target
        /*
         * var sourceTopicCategories = dbContext.TopicCategories.AsEnumerable(); 
         * For AsEnumable(), a Select query continues to occur 
         * in the Where conditional statement due to Lazy loading
         * AsEnumable() -> ToList()
         */
        var sourceTopicCategories = dbContext.TopicTags.ToList();
        var TopicTagNames = Enum.GetNames(typeof(Tag)).ToArray();

        
        var newCategories = TopicTagNames
            .Where(name => !sourceTopicCategories.Any(stc => stc.Name == name))
            .Select(name => {
                Tag enumValue;
                Enum.TryParse<Tag>(name, out enumValue);
                return TopicTag.Create(enumValue);
            }); 

        dbContext.TopicTags.AddRange(newCategories);
        dbContext.SaveChanges();
 
    }
}
