using FluentAssertions;
using JCommunity.AppCore.Core.Utils;
using JCommunity.AppCore.Entities.Member;
using JCommunity.Test.Core.Utils;

namespace JCommunity.Test.Core.Entities;

public class MemberEntityTest
{

	MemoryDbContext _dbContext = new MemoryDbContext();

	const string NAME = "Ji-Woong Hwang";
	const string NICKNAME = "102boy";
	const string EMAIL = "carrena@naver.com";
	const string PASSWORD = "Pa$$w0rd";

    [Fact]
	void Member_Create_Test()
	{
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);

        // Act
        _dbContext.Members.Add(member);
		_dbContext.SaveChanges();


		// Assert
		_dbContext.Members.Should().Contain(m => m.Id == member.Id);
	}

	[Fact]
	void Member_Read_Test()
	{
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        _dbContext.Members.Add(member);
		_dbContext.SaveChanges();

        // Act
		var findMember = _dbContext.Members.Find(member.Id);

		// Assert
		findMember.Should().NotBeNull();
		findMember.Should().BeEquivalentTo(member);
	}

	[Fact]
	void Member_Update_NickName_Test()
	{
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        _dbContext.Members.Add(member);
        _dbContext.SaveChanges();

		// Act
		var newNickname = $"{NICKNAME}_Changed_Value";
		var findMember = _dbContext.Members.Find(member.Id);

		if (findMember == null) return;
		findMember.UpdateNickname(newNickname);
		_dbContext.SaveChanges();

		// Assert
		_dbContext.Members.Should().Contain(m => m.Id.Equals(member.Id) && m.NickName.Equals(newNickname));
    }

    [Fact]
    void Member_Update_Email_Test()
    {
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        _dbContext.Members.Add(member);
        _dbContext.SaveChanges();

        // Act
        var newEmail = $"{EMAIL}_Changed_Value";
        var findMember = _dbContext.Members.Find(member.Id);

        if (findMember == null) return;
        findMember.UpdateEmail(newEmail);
        _dbContext.SaveChanges();

		// Assert
		_dbContext.Members.Should().Contain(m => m.Id.Equals(member.Id) && m.Email.Equals(newEmail));
    }

    [Fact]
    void Member_Update_Password_Test()
    {
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        _dbContext.Members.Add(member);
        _dbContext.SaveChanges();

        // Act
        var newPassword = $"{PASSWORD}_Changed_Value";
        var hashed = PasswordHasher.HashPassword(newPassword);
        var findMember = _dbContext.Members.Find(member.Id);

        if (findMember == null) return;
        findMember.UpdatePassword(newPassword);
        _dbContext.SaveChanges();

        // Assert
        _dbContext.Members.Should().Contain(m => m.Id.Equals(member.Id) && m.Password.Equals(hashed));
    }

    [Fact]
    void Member_Delete_Test()
    {
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);
        _dbContext.Members.Add(member);
        _dbContext.SaveChanges();

        // Act
        _dbContext.Members.Remove(member);
        _dbContext.SaveChanges();

        // Assert
        _dbContext.Members.Should().NotContain(m => m.Id.Equals(member.Id));
    }

    [Fact]
    void Member_GetId_Test()
    {
        // Arrange
        Member member = Member.Create(NAME, NICKNAME, PASSWORD, EMAIL);

        // Act
        var id = member.GetMemberId();

        // Assert
        id.Should().Be(member.Id.id.ToString());
    }
}

