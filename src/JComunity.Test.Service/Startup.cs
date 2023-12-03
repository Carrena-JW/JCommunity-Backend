namespace JComunity.Test.Service;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddValidatorsFromAssembly(typeof(Web.Contract.AssemblyReference).Assembly);
    }
}
