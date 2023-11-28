using JComunity.AppCore.Utils;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace JComunity.Test.Core.Utils
{
    public class 유틸_테스트
    {
        [Fact]
        public void 해시_암호화_테스트()
        {
            // Arrange
            const string 비밀번호 = "1";

            //Act
            var hashedPassword = PasswordHasher.HashPassword(비밀번호);
            var isSame = PasswordHasher.VerifyPassword(비밀번호, hashedPassword);

            // Assert
            Assert.True(isSame);
        }
    }
}