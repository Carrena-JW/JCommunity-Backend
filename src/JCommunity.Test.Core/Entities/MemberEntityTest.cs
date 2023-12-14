using FluentAssertions;
using JCommunity.AppCore.Core.Utils;
using JCommunity.AppCore.Entities.Member;
using JCommunity.Test.Core.Utils;

namespace JCommunity.Test.Core.Entities;

public class MemberEntityTest
{
	const string NAME = "Ji-Woong Hwang";
	const string NICKNAME = "102boy";
	const string EMAIL = "carrena@naver.com";
	const string PASSWORD = "Pa$$w0rd";

    [Fact]
	void Member_Create_Test()
	{
        using MemoryDbContext dbContext = new MemoryDbContext();
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);

        // Act
        dbContext.Members.Add(member);
		dbContext.SaveChanges();


		// Assert
		dbContext.Members.Should().Contain(m => m.Id == member.Id);
	}

	[Fact]
	void Member_Read_Test()
	{
        using MemoryDbContext dbContext = new MemoryDbContext();

        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        dbContext.Members.Add(member);
		dbContext.SaveChanges();

        // Act
		var findMember = dbContext.Members.Find(member.Id);

		// Assert
		findMember.Should().NotBeNull();
		findMember.Should().BeEquivalentTo(member);
	}

	[Fact]
	void Member_Update_NickName_Test()
	{
        using MemoryDbContext dbContext = new MemoryDbContext();
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        dbContext.Members.Add(member);
        dbContext.SaveChanges();

		// Act
		var newNickname = $"{NICKNAME}_Changed_Value";
		var findMember = dbContext.Members.Find(member.Id);

		if (findMember == null) return;
		findMember.UpdateNickname(newNickname);
		dbContext.SaveChanges();

		// Assert
		dbContext.Members.Should().Contain(m => m.Id.Equals(member.Id) && m.NickName.Equals(newNickname));
    }

    [Fact]
    void Member_Update_Email_Test()
    {
        using MemoryDbContext dbContext = new MemoryDbContext();
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        dbContext.Members.Add(member);
        dbContext.SaveChanges();

        // Act
        var newEmail = $"{EMAIL}_Changed_Value";
        var findMember = dbContext.Members.Find(member.Id);

        if (findMember == null) return;
        findMember.UpdateEmail(newEmail);
        dbContext.SaveChanges();

		// Assert
		dbContext.Members.Should().Contain(m => m.Id.Equals(member.Id) && m.Email.Equals(newEmail));
    }

    [Fact]
    void Member_Update_Password_Test()
    {
        using MemoryDbContext dbContext = new MemoryDbContext();

        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        dbContext.Members.Add(member);
        dbContext.SaveChanges();

        // Act
        var newPassword = $"{PASSWORD}_Changed_Value";
        var hashed = PasswordHasher.HashPassword(newPassword);
        var findMember = dbContext.Members.Find(member.Id);

        if (findMember == null) return;
        findMember.UpdatePassword(newPassword);
        dbContext.SaveChanges();

        // Assert
        dbContext.Members.Should().Contain(m => m.Id.Equals(member.Id) && m.Password.Equals(hashed));
    }

    [Fact]
    void Member_Delete_Test()
    {
        using MemoryDbContext dbContext = new MemoryDbContext();

        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        dbContext.Members.Add(member);
        dbContext.SaveChanges();

        // Act
        dbContext.Members.Remove(member);
        dbContext.SaveChanges();

        // Assert
        dbContext.Members.Should().NotContain(m => m.Id.Equals(member.Id));
    }

    [Fact]
    void Member_GetId_Test()
    {
        using MemoryDbContext dbContext = new MemoryDbContext();

        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);

        // Act
        var id = member.GetMemberId();

        // Assert
        id.Should().Be(member.Id.ToString());
    }
}

