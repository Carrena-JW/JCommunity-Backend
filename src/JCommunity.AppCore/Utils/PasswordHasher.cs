

namespace JCommunity.AppCore.Utils;

public static class PasswordHasher
{
    private const int NOMAL_PASSWORD_NOT_OVER = 20;

    public static string HashPassword(string password)
    {
        using (SHA512 s512 = SHA512.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = s512.ComputeHash(passwordBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public static bool VerifyPassword(string userInputPassword, string storedHashedPassword)
    {
        string userInputHashed = HashPassword(userInputPassword);
        return storedHashedPassword.Equals(userInputHashed);
    }

    public static bool IsHashed(string value) =>
        value.Length > NOMAL_PASSWORD_NOT_OVER ? true : false;
}
