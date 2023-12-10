namespace JCommunity.Test.Core.Utils;

public class TestUtil
{
    public static string GenerateRandomString(int length, bool isEmailType=false)
    {
        // 문자열 또는 이메일을 위한 랜덤 값 생성 로직
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        if (isEmailType)
        {
            string username = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string domain = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{username}@{domain}.com";
        }
        else
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public static int GenerateRandomInt(int length)
    {
        // 정수를 위한 랜덤 값 생성 로직
        Random random = new Random();
        int minValue = (int)Math.Pow(10, length - 1);
        int maxValue = (int)Math.Pow(10, length) - 1;
        return random.Next(minValue, maxValue + 1);
    }
}

