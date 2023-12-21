using MediatR;

namespace JCommunity.Infrastructure.Setup;

public class SetupTestMemberSeed 
{
    public static void Setup(IServiceProvider serviceProvider)
    {

        using var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(),
            serviceProvider.GetRequiredService<IMediator>());

        // Setup Target
        /*
         * var sourceTopicCategories = dbContext.TopicCategories.AsEnumerable(); 
         * For AsEnumable(), a Select query continues to occur 
         * in the Where conditional statement due to Lazy loading
         * AsEnumable() -> ToList()
         */
        var allUsers = dbContext.Members.ToList();
        var TopicTagNames = Enum.GetNames(typeof(Tag)).ToArray();

        Member[] users = {
            Member.Create("Ji-Woong", "102boy", "Pa$$w0rd", "102boy@mail.coom"),
            Member.Create("Ji-Woong", "102boy1", "Pa$$w0rd", "102boy_1@mail.coom"),
            Member.Create("Ji-Woong", "102boy2", "Pa$$w0rd", "102boy_2@mail.coom"),
            Member.Create("Ji-Woong", "102boy3", "Pa$$w0rd", "102boy_3@mail.coom"),
            Member.Create("Ji-Woong", "102boy4", "Pa$$w0rd", "102boy_4@mail.coom"),
        };
         

        var targets = users.Where(a => !allUsers.Any(u => u.NickName == a.NickName)).ToArray();

        dbContext.Members.AddRange(targets);
        dbContext.SaveChanges();
 
    }
}
