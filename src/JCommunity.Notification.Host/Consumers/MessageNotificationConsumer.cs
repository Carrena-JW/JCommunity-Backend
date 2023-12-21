namespace JCommunity.Notification.Host.Consumers;

public class MessageNotificationConsumer : IConsumer<QueueRecord>
{
    private readonly ILogger<MessageNotificationConsumer> _logger;
    private readonly ISomeService _someService;
    private readonly IPushService _pushService;
    private readonly IEmailService _emailService;
    private readonly IWebRequestService _webRequestService;

    public MessageNotificationConsumer(
        ILogger<MessageNotificationConsumer> logger, 
        IWebRequestService webRequestService, 
        IEmailService emailService, 
        IPushService pushService, 
        ISomeService someService)
    {
        _logger = logger;
        _webRequestService = webRequestService;
        _emailService = emailService;
        _pushService = pushService;
        _someService = someService;
    }

    public async Task Consume(ConsumeContext<QueueRecord> context)
    {
        _logger.LogInformation($"Recevied Message : {@context.Message}");

        // You can process the queue message received as follows.
        var fromId = Guid.Parse(context.Message.fromId);
        
        await _someService.SendSomeJobAsync(fromId, new());
        await _pushService.SendSomeJobAsync(fromId, new());
        await _emailService.SendSomeJobAsync(fromId, new());
        await _webRequestService.SendSomeJobAsync(fromId, new());

        return;
    }

}
