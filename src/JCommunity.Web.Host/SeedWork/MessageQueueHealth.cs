namespace JCommunity.Web.Host.SeedWork;

public class MessageQueueHealth
{
    public static HealthCheckResult Checker()
    {
        return HealthCheckResult.Healthy();
    }
}
