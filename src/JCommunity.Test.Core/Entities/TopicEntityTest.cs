
using FluentAssertions;
using JCommunity.AppCore.Entities.Member;
using JCommunity.AppCore.Entities.Topics;
using JCommunity.Test.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace JCommunity.Test.Core.Entities;

public class TopicEntityTest
{

    

    const string NAME = "TOPIC_NAME";
    const string DESCRIPTION = "This is Description";
    const int SROT = 1;
    
    const string MEMBER_NAME = "Ji-Woong Hwang";
	const string MEMBER_NICKNAME = "102boy";
	const string MEMBER_EMAIL = "carrena@naver.com";
	const string MEMBER_PASSWORD = "Pa$$w0rd";

    Member CreatedMember = default!;

    void Init_Job(MemoryDbContext dbContext)
    {
        // Memory database 초기화
         

        CreatedMember = Member.Create(MEMBER_NAME, MEMBER_NICKNAME, MEMBER_PASSWORD, MEMBER_EMAIL);
        dbContext.Members.Add(CreatedMember);
        dbContext.SaveChanges();

    }


    [Fact]
	void Topic_Create_Test()
	{
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);

        // Act
        dbContext.Topics.Add(topic);
        dbContext.SaveChanges();
        var createdTopic = dbContext.Topics.AsNoTracking().First();

		// Assert
        topic.Id.Should().Be(createdTopic.Id);
	}

    [Fact]
    void Topic_Add_Tag_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        var tag = TopicTag.Create(Tag.Whatever);
        topic.AddTag(tag);

        // Act
        dbContext.Topics.Add(topic);
        dbContext.SaveChanges();
        var createdTopic = dbContext.Topics
            .Include(t=> t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().NotBeEmpty();
        createdTopic.Tags.Should().Contain(t=> t.Value == tag.Value );

    }

    [Fact]
    void Topic_AddRange_Tags_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);

        var tags = new TopicTag[]
        {
            TopicTag.Create(Tag.Whatever),
            TopicTag.Create(Tag.Economy),
            TopicTag.Create(Tag.Finance)
        };
        topic.AddTags(tags);
        topic.AddTags(tags);

        // Act
        dbContext.Topics.Add(topic);
        dbContext.SaveChanges();
        var createdTopic = dbContext.Topics
            .Include(t => t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().NotBeEmpty();
        createdTopic.Tags.Should().HaveCountGreaterThanOrEqualTo(3);

    }
    [Fact]
    void Topic_Remove_Tag_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        var tag1 = TopicTag.Create(Tag.Whatever);
        topic.AddTag(tag1);

        // Act
        topic.RemoveTag(tag1);
        dbContext.Topics.Add(topic);
        dbContext.SaveChanges();
        var createdTopic = dbContext.Topics
            .Include(t => t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().BeEmpty();
    }

    [Fact]
    void Topic_Remove_All_Tag_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        var tag1 = TopicTag.Create(Tag.Whatever);
        var tag2 = TopicTag.Create(Tag.Whatever);
        var tag3 = TopicTag.Create(Tag.Whatever);
        topic.AddTag(tag1);
        topic.AddTag(tag2);
        topic.AddTag(tag3);

        // Act
        topic.RemoveAllTags();
        dbContext.Topics.Add(topic);
        dbContext.SaveChanges();
        var createdTopic = dbContext.Topics
            .Include(t => t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().BeEmpty();
    }

    [Fact]
    void Topic_Update_Name_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        dbContext.Topics.Add(topic);

        // Act
        var updatedName = "UpdatedName";
        topic.UpdateTopicName(updatedName);
        dbContext.SaveChanges();

        var createdTopic = dbContext.Topics.AsNoTracking().First();

        // Assert
        createdTopic.Name.Should().Be(updatedName);
    }

    [Fact]
    void Topic_Update_Desc_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        dbContext.Topics.Add(topic);

        // Act
        var updatedDesc = "UpdatedDesc";
        topic.UpdateTopicDescription(updatedDesc);
        dbContext.SaveChanges();

        var createdTopic = dbContext.Topics.AsNoTracking().First();

        // Assert
        createdTopic.Description.Should().Be(updatedDesc);
    }

    [Fact]
    void Topic_Update_SortOrder_Test()
    {
        // Setup
        using var dbContext = new MemoryDbContext();
        Init_Job(dbContext);

        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        dbContext.Topics.Add(topic);

        // Act
        var sortOrder = 3;
        topic.UpdateTopicSortOrder(sortOrder);
        dbContext.SaveChanges();

        var createdTopic = dbContext.Topics.AsNoTracking().First();

        // Assert
        createdTopic.Sort.Should().Be(sortOrder);
    }


}

