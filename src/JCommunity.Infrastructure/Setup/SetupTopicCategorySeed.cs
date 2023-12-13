using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JCommunity.Infrastructure.Setup;

public static class SetupTopicCategorySeed 
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
        var sourceTopicCategories = dbContext.TopicCategories.ToList();
        var topicCategoryNames = Enum.GetNames(typeof(Category)).ToArray();

        var takeTop10 = dbContext.TopicCategories.AsEnumerable();

        var newCategories = topicCategoryNames
            .Where(name => !sourceTopicCategories.Any(stc => stc.Name == name))
            .Select(name => TopicCategory.Create(name));

        dbContext.TopicCategories.AddRange(newCategories);
        dbContext.SaveChanges();
 
    }
}
