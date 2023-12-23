namespace JCommunity.Notification.Host.Services;

public interface IPushService : INotificationService
{
    Task SendSomeJobAsync(Ulid id, CancellationToken token);
}
public class PushService : IPushService
{
    private ILogger<PushService> _logger;

    public PushService(ILogger<PushService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task SendSomeJobAsync(Ulid id, CancellationToken token)
    {
        _logger.LogInformation($"Sendding Push : {id}");
        return Task.CompletedTask;
    }
}
