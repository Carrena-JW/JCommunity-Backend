namespace JComunity.Web.Host.ApiEndpoints;

public class MemberApiService
{
    public ILogger<MemberApiService> logger { get;  }
    public MemberApiService(ILogger<MemberApiService> logger)
    {
        this.logger = logger;
    }

}
