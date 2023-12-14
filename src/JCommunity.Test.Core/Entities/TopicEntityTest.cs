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

    Member CreatedMember;

    void Seed_Job()
    {
        CreatedMember = Member.Create(MEMBER_NAME, MEMBER_NICKNAME, MEMBER_PASSWORD, MEMBER_EMAIL);
        _dbContext.Members.Add(CreatedMember);
        _dbContext.SaveChanges();

    }


    [Fact]
	void Topic_Create_Test()
	{
        Seed_Job();
        // Arrange
        var topic = Topic.Create(NAME, DESCRIPTION, SROT, CreatedMember.Id);
        _dbContext.Topics.Add(topic);
        _dbContext.SaveChanges();


        //_dbContext.Members.Should().Contain(m => m.Id == member.Id);

        var createdTopic = _dbContext.Topics.AsNoTracking().First();

         
        // Act


		// Assert
	}
     
}

