namespace JCommunity.Notification.Host.Services;

public interface ISomeService : INotificationService
{
    Task SendSomeJobAsync(Guid id, CancellationToken token);
}
public class SomeService : ISomeService
{
    private ILogger<SomeService> _logger;

    public SomeService(ILogger<SomeService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task SendSomeJobAsync(Guid id, CancellationToken token)
    {
        _logger.LogInformation($"Sendding SomeJob : {id}");
        return Task.CompletedTask;
    }
}
