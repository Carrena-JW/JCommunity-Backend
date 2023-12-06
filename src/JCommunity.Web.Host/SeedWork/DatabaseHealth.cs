namespace JCommunity.Web.Host.SeedWork;

public class DatabaseHealth
{
    public static HealthCheckResult Checker()
    {
        return HealthCheckResult.Healthy();
    }
}
