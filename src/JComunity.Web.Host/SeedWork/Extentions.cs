using JComunity.Web.Host.Utils;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace JComunity.Web.Host.SeedWork;

public static class Extentions
{
    public static IServiceCollection AddWebHostServices(
        this IServiceCollection services,
        IConfiguration config,
        IWebHostEnvironment env)
    {
        var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

        services.AddSerilog(conf =>
        {
          
            
            conf
#if DEBUG
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
#endif
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")))
            .ReadFrom.Configuration(configuration);
        });

        


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

     

        //Validator DI
        services.AddValidatorsFromAssembly(typeof(Web.Contract.AssemblyReference).Assembly);


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

        app.MapGet("/health/report",async () =>
        {
            var healthCheckService = app.Services.GetRequiredService<HealthCheckService>();
            var result = await healthCheckService.CheckHealthAsync();
            return Results.Ok(new { Environment.MachineName, result});
        });
        #endregion

        return app;
    }

}
