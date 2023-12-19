using FluentAssertions;
using JCommunity.AppCore.Entities.MemberAggregate;
using JCommunity.AppCore.Entities.PostAggregate;
using JCommunity.AppCore.Entities.TopicAggregate;
using JCommunity.Test.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace JCommunity.Test.Core.Entities;

public class PostEntityTest
{
    const string TOPIC_NAME = "TOPIC_NAME";
    const string TOPIC_DESCRIPTION = "This is Description";
    const int TOPIC_SROT = 1;

    const string MEMBER_NAME = "Ji-Woong Hwang";
    const string MEMBER_NICKNAME = "102boy";
    const string MEMBER_EMAIL = "carrena@naver.com";
    const string MEMBER_PASSWORD = "Pa$$w0rd";

    Member CreatedMember = null!;
    Topic CreatedTopic = null!;

    void Init_Job(MemoryDbContext dbContext)
    {
        // Member
        CreatedMember = Member.Create(MEMBER_NAME, MEMBER_NICKNAME, MEMBER_PASSWORD, MEMBER_EMAIL);
        dbContext.Members.Add(CreatedMember);
        dbContext.SaveChanges();

        // Topic
        CreatedTopic = Topic.Create(TOPIC_NAME, TOPIC_DESCRIPTION, TOPIC_SROT, CreatedMember.Id);
        dbContext.Topics.Add(CreatedTopic);
        dbContext.SaveChanges();
    }

    [Fact]
    void Post_Create_Draft_Test()
    {
        using MemoryDbContext dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var title = "this is title";
        var htmlBody = "this is body";
        var sources = "this is sources";

        var attachment = new PostContentAttachment();

        var post = Post.Create(
            CreatedTopic.Id, 
            title, 
            htmlBody, 
            sources, 
            CreatedMember.Id, 
            attachment);

        dbContext.Posts.Add(post);
        dbContext.SaveChanges();

        // Act
        var findPost = dbContext.Posts.AsNoTracking()
            .Include(x=> x.Contents)
            .Include(x=> x.Author)
            .Include(x=> x.Topic)
            .SingleOrDefault(x => x.Id == post.Id);

        // Assert
        findPost.Should().NotBeNull();
        post.Id.Should().Be(findPost!.Id);
    }


}
