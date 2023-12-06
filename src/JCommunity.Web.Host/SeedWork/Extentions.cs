using JCommunity.Web.Host.ApiEndpoints.Member;
using JCommunity.Web.Host.SeedWork.Filters;
using Microsoft.Extensions.Configuration;

namespace JCommunity.Web.Host.SeedWork;

public static class Extentions
{
  
    public static IServiceCollection AddWebHostServices(
        this IServiceCollection services,
        IConfiguration config,
        IWebHostEnvironment env)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(env.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json")
            .Build();
        

        services.AddSerilog(conf =>
        {
            var elasticServerUri = config.GetSection("ElasticConfiguration").GetSection("Uri").Value ?? "http://localhost:9200";
            var elasticsearchOptions = new ElasticsearchSinkOptions(new Uri(elasticServerUri));

            conf
#if DEBUG
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
#endif
            .WriteTo.Elasticsearch(elasticsearchOptions)
            .ReadFrom.Configuration(configuration);
        });


        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(config.GetConnectionString("PostgresConnection")));
        

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        // server health check
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddCheck("database", () => DatabaseHealth.Checker())
            .AddCheck("message_queue", () => MessageQueueHealth.Checker());



        return services;
    }

    public static IApplicationBuilder UseWebHostApplications(
        this WebApplication app,
        IWebHostEnvironment env)
    {
        #region [Open API]
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        #endregion

        #region [Global Filters]
        var rootMapPath = app.MapGroup(string.Empty)
           .AddEndpointFilter<GlobalLoggingFilter>();
        #endregion

        #region [Map Endpoints]

        rootMapPath.MapGroup("/api/v1/Member")
            .WithTags(new[] { "Member Query API" })
            .MapMemberQueryApi();


        rootMapPath.MapGroup("/api/v1/Member")
            .WithTags(new[] { "Member Command API" })
            .MapMemberCommandApi();
        #endregion

        #region [Health check]
        app.MapHealthChecks("/health");

        app.MapGet("/health/report", async () =>
        {
            var healthCheckService = app.Services.GetRequiredService<HealthCheckService>();
            var result = await healthCheckService.CheckHealthAsync();
            return Results.Ok(new { Environment.MachineName, result });
        });
        #endregion

        return app;
    }

}
