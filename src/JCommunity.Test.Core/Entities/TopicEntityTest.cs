using FluentAssertions;
using JCommunity.AppCore.Entities.Member;
using JCommunity.AppCore.Entities.Topics;
using JCommunity.Test.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace JCommunity.Test.Core.Entities;

public class TopicEntityTest
{

	MemoryDbContext _dbContext = new MemoryDbContext();

    const string NAME = "TOPIC_NAME";
    const string DESCRIPTION = "This is Description";
    const int SROT = 1;
    const string PASSWORD = "Pa$$w0rd";
    
    const string MEMBER_NAME = "Ji-Woong Hwang";
	const string MEMBER_NICKNAME = "102boy";
	const string MEMBER_EMAIL = "carrena@naver.com";
	const string MEMBER_PASSWORD = "Pa$$w0rd";

    Member CreatedMember = default!;

    void Init_Job()
    {
        CreatedMember = Member.Create(MEMBER_NAME, MEMBER_NICKNAME, MEMBER_PASSWORD, MEMBER_EMAIL);
        _dbContext.Members.Add(CreatedMember);
        _dbContext.SaveChanges();

    }


    [Fact]
	void Topic_Create_Test()
	{
        Init_Job();
        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);

        // Act
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();
        var createdTopic = _dbContext.Topics.AsNoTracking().First();

		// Assert
        topic.Id.Should().Be(createdTopic.Id);
	}

    [Fact]
    void Topic_Add_Tag_Test()
    {
        Init_Job();
        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        var tag = TopicTag.Create(Tag.Whatever);
        topic.AddTag(tag);

        // Act
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();
        var createdTopic = _dbContext.Topics
            .Include(t=> t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().NotBeEmpty();
        createdTopic.Tags.Should().Contain(t=> t.Value == tag.Value );

    }

    [Fact]
    void Topic_AddRange_Tags_Test()
    {
        Init_Job();
        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);

        var tags = new TopicTag[]
        {
            TopicTag.Create(Tag.Whatever),
            TopicTag.Create(Tag.Economy),
            TopicTag.Create(Tag.Finance)
        };
        topic.AddTags(tags);

        // Act
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();
        var createdTopic = _dbContext.Topics
            .Include(t => t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().NotBeEmpty();
        createdTopic.Tags.Should().HaveCountGreaterThanOrEqualTo(3);

    }
    [Fact]
    void Topic_Remove_Tag_Test()
    {
        Init_Job();
        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        var tag1 = TopicTag.Create(Tag.Whatever);
        topic.AddTag(tag1);

        // Act
        topic.RemoveTag(tag1);
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();
        var createdTopic = _dbContext.Topics
            .Include(t => t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().BeEmpty();
    }

    [Fact]
    void Topic_Remove_All_Tag_Test()
    {
        Init_Job();
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
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();
        var createdTopic = _dbContext.Topics
            .Include(t => t.Tags)
            .AsNoTracking().First();

        // Assert
        createdTopic.Tags.Should().BeEmpty();
    }



}

