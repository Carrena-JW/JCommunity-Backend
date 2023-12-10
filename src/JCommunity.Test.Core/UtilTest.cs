using FluentAssertions;
using JCommunity.AppCore.Core.Utils;
using JCommunity.Test.Core.Utils;

namespace JCommunity.Test.Core;

public class UtilTest
{
    [Fact]
    void Password_Hashed_Test()
    {
        // Arrange
        const string 비밀번호 = "1";

        //Act
        var hashedPassword = PasswordHasher.HashPassword(비밀번호);
        var isSame = PasswordHasher.VerifyPassword(비밀번호, hashedPassword);

        // Assert
        Assert.True(isSame);
    }

    [Fact]
    void Password_Is_Hashed_Test()
    {
        // Arrange
        var password = "Pa$$$$w0rd";
        var hashedPassword = PasswordHasher.HashPassword(password);

        // Act
        var result = PasswordHasher.IsHashed(hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    void Password_Compare_Hashed_Test()
    {
        // Arrange
        var password = TestUtil.GenerateRandomString(10);

        // Act
        var hashed1 = PasswordHasher.HashPassword(password);
        var hashed2 = PasswordHasher.HashPassword(password);

        // Assert
        hashed1.Length.Should().Be(hashed2.Length);
        hashed1.Should().Be(hashed2);
    }
}
