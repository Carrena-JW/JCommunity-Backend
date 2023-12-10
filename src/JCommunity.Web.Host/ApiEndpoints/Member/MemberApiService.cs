namespace JCommunity.Web.Host.ApiEndpoints.Member;

internal class MemberApiService(
    ILogger<MemberApiService> logger,
    ISender mediator)
{
    public ILogger<MemberApiService> logger { get; } = logger;
    public ISender Mediator { get; } = mediator;
}
