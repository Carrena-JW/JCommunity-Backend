namespace JCommunity.Services.PostService.EventHandler;

internal class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
{
    private readonly ILogger<PostCreatedEventHandler> _logger;
    private readonly IMessagePublisherService _publisher;

    public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger, 
        IMessagePublisherService publisher)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task Handle(PostCreatedEvent notification, CancellationToken token)
    {
        var record = new QueueRecord(
            notification.EventId.ToString(), 
            QueueRecordType.PostCreated, 
            notification.PostId.ToString());

        await _publisher.SendNotificationAsync(record,token);
        _logger.LogInformation("Sendding Notification PostCreated Message : {@notification.EventId}", notification.EventId);

        return;
    }
}
