namespace JCommunity.Web.Host.ApiEndpoints.Member;

public class MemberApiService(
    ILogger<MemberApiService> logger,
    IMediator mediator)
{
    public ILogger<MemberApiService> logger { get; } = logger;
    public IMediator Mediator { get; set; } = mediator;
}
