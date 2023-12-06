namespace JCommunity.Test.Service;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddValidatorsFromAssembly(typeof(Services.AssemblyReference).Assembly);
    }
}
