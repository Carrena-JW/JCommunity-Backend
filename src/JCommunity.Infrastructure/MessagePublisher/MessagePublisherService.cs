namespace JCommunity.Infrastructure.MessagePublisher
{
    public interface IMessagePublisherService
    {
        Task SendNotificationAsync(QueueRecord record, CancellationToken token);
    }

    public class MessagePublisherService : IMessagePublisherService
    {
        private ILogger<MessagePublisherService> _logger;
        private IPublishEndpoint _publishEndpoint;

        public MessagePublisherService(ILogger<MessagePublisherService> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task SendNotificationAsync(QueueRecord record, CancellationToken token)
        {
            await _publishEndpoint.Publish(record, token);
            _logger.LogInformation("Published Message : {@record}", record);
            return;
        }
    }
}
