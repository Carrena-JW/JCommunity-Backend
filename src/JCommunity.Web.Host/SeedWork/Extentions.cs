using JCommunity.Web.Host.ApiEndpoints.File;
using JCommunity.Web.Host.ApiEndpoints.Topic;

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

        var connectionStrings = config.GetConnectionString("PostgresConnection") ?? "http://localhost:5432";

        services.AddDbContext<AppDbContext>(
            options =>options.UseNpgsql(connectionStrings));

         

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            //Resolve schema redundancy due to nested classes
            options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
        });


        // server health check
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddNpgSql(connectionString: connectionStrings,
                       name: "PostgreSQL",
                       failureStatus: HealthStatus.Degraded,
                       tags: new string[] { "db", "postgresql" }
                       )
            .AddCheck("message_queue", () => MessageQueueHealth.Checker());

        //global exception handler
        services.AddExceptionHandler<GlobalUnhandleExceptionHandler>();
        services.AddProblemDetails();

       


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

        #region [Global Exception Handler]
        app.UseExceptionHandler();
        #endregion

        #region [Global Filters]
        var rootMapPath = app.MapGroup(string.Empty)
           .AddEndpointFilter<GlobalLoggingFilter>();
        #endregion

        #region [Map Endpoints]

        rootMapPath.MapGroup("/api/v1/Members")
            .WithTags(new[] { "Member Query API" })
            .MapMemberQueryApi();


        rootMapPath.MapGroup("/api/v1/Members")
            .WithTags(new[] { "Member Command API" })
            .MapMemberCommandApi();

        rootMapPath.MapGroup("/api/v1/Topics")
            .WithTags(new[] { "Topic Command API" })
            .MapTopicCommandApi();

        rootMapPath.MapGroup("/api/v1/Topics")
            .WithTags(new[] { "Topic Query API" })
            .MapTopicQueryApi();

        rootMapPath.MapGroup("/api/v1/file")
            .WithTags(new[] { "File API" })
            .MapFileApi();
        #endregion

        #region [Health check]
        app.MapHealthChecks("/health");

        app.MapGet("/health/report", async () =>
        {
            var healthCheckService = app.Services.GetRequiredService<HealthCheckService>();
            var result = await healthCheckService.CheckHealthAsync();

            /** 2023-12-10 
            * In the event of an exception, the target site has symptoms that do not normally allow Json serialization, 
            * so creating a CustomReport class is added to re-bind the problematic Exception item to the default Exception
            */
            object resultReport = result.Status == HealthStatus.Healthy ?
                                  result :
                                  CustomHealthReport.Create(result);
            
            return Results.Ok(new { Environment.MachineName, resultReport });
        });
        #endregion

        #region [Setup Seed Data]
        var enableSeedJob = app.Configuration.GetValue("EnableSetupSeed", false);
             

        if (enableSeedJob)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            SetupTestMemberSeed.Setup(services);
        }
        #endregion


       

        return app;
    }


     
}
