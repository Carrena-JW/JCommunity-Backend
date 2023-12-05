namespace JCommunity.Web.Host.Utils;

public class DatabaseHealth
{
    public static HealthCheckResult Checker()
    {
        return HealthCheckResult.Healthy();
    }
}
