using JComunity.AppCore.Utils;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace JComunity.Test.Core.Utils
{
    public class ��ƿ_�׽�Ʈ
    {
        [Fact]
        public void �ؽ�_��ȣȭ_�׽�Ʈ()
        {
            // Arrange
            const string ��й�ȣ = "1";

            //Act
            var hashedPassword = PasswordHasher.HashPassword(��й�ȣ);
            var isSame = PasswordHasher.VerifyPassword(��й�ȣ, hashedPassword);

            // Assert
            Assert.True(isSame);
        }
    }
}