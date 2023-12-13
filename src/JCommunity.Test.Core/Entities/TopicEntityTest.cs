using FluentAssertions;
using JCommunity.AppCore.Entities.Member;
using JCommunity.Test.Core.Utils;

namespace JCommunity.Test.Core.Entities;

public class TopicEntityTest
{

	MemoryDbContext _dbContext = new MemoryDbContext();

    const string NAME = "TOPIC_NAME";
    const string Description = "This is Description";
    const int SROT = 1;
    const string PASSWORD = "Pa$$w0rd";
    
    const string MEMBER_NAME = "Ji-Woong Hwang";
	const string MEMBER_NICKNAME = "102boy";
	const string MEMBER_EMAIL = "carrena@naver.com";
	const string MEMBER_PASSWORD = "Pa$$w0rd";


    void Seed_Job()
    {
        Member member = Member.Create(MEMBER_NAME, MEMBER_NICKNAME, MEMBER_PASSWORD, MEMBER_EMAIL);
        _dbContext.Members.Add(member);
        _dbContext.SaveChanges();

    }


    [Fact]
	void Topic_Create_Test()
	{
        Seed_Job();
        // Arrange
        // Create Author
     
		//_dbContext.Members.Should().Contain(m => m.Id == member.Id);



        // Act


		// Assert
	}
     
}

