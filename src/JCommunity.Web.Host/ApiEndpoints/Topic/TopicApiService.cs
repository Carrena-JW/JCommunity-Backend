namespace JCommunity.Web.Host.ApiEndpoints.Topic;

internal class TopicApiService(
	ILogger<TopicApiService> logger,
	ISender mediator)
{
	public ILogger<TopicApiService> Logger { get; } = logger;
	public ISender Mediator { get; } = mediator;
}

