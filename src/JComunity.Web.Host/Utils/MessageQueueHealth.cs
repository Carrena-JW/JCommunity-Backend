﻿namespace JComunity.Web.Host.Utils;

public class MessageQueueHealth
{
    public static HealthCheckResult Checker()
    {
        return HealthCheckResult.Healthy();
    }
}
