namespace JCommunity.Infrastructure.MessagePublisher
{
    public interface IMessagePublisherService
    {
        Task SendNotificationAsync(QueueRecord record, CancellationToken token);
    }

    public class MessagePublisherService : IMessagePublisherService
    {
        private readonly ILogger<MessagePublisherService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBusControl _busControl;

        public MessagePublisherService(
            ILogger<MessagePublisherService> logger, 
            IPublishEndpoint publishEndpoint,
            IBusControl busControl)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        }

        public async Task SendNotificationAsync(QueueRecord record, CancellationToken token)
        {
            Action<QueueRecord> addTemporaryMemory = (r) => {
                _logger.LogWarning($"Occur connection problems, save to temporary memory storage : {record} ");
                TemporaryMemoryStorage.AddRecord(r.id, r);
            };

            var status = _busControl.CheckHealth().Status;
            if (status != BusHealthStatus.Healthy)
            {
                addTemporaryMemory(record);
                return;
            }

            //When the connection is restored,
            //it will send out unprocessed messages in batches. 
            if (TemporaryMemoryStorage.StoredRecordCount() > 0)
            {
                _ = ReSendNotification();
            }

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3)); // Set timeout here
            CancellationToken timeoutToken = cts.Token;

            try
            {
                await _publishEndpoint.Publish(record, timeoutToken);
            }
            catch (OperationCanceledException) when (timeoutToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Timeout while publish message : {record}");
                addTemporaryMemory(record);
            }

            _logger.LogInformation("Published Message : {@record}", record);
            return;
        }

        private async Task ReSendNotification(CancellationToken token = new())
        {
            var items = TemporaryMemoryStorage.GetAllItems();

            foreach(var record in items)
            {
                await _publishEndpoint.Publish(record.Value, token);
                _logger.LogInformation("Re-Published Message : {@record}", record);
                TemporaryMemoryStorage.RemoveRecord(record.Key);
            }
        }
    }
}
