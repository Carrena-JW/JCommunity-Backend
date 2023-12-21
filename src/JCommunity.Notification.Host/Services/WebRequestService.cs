namespace JCommunity.Notification.Host.Services;

public interface IWebRequestService : INotificationService
{
    Task SendSomeJobAsync(Guid id, CancellationToken token);
}
public class WebRequestService : IWebRequestService
{
    private ILogger<WebRequestService> _logger;

    public WebRequestService(ILogger<WebRequestService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task SendSomeJobAsync(Guid id, CancellationToken token)
    {
        _logger.LogInformation($"Sendding Webrequest : {id}");
        return Task.CompletedTask;
    }
}
