namespace JCommunity.Notification.Host.Services;

public interface IEmailService : INotificationService
{
    Task SendSomeJobAsync(Ulid id, CancellationToken token);
}
public class EmailService : IEmailService
{
    private ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task SendSomeJobAsync(Ulid id, CancellationToken token)
    {
        _logger.LogInformation($"Sendding Email : {id}");
        return Task.CompletedTask;
    }
}
