var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<ISomeService, SomeService>();
builder.Services.AddScoped<IPushService, PushService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IWebRequestService, WebRequestService>();

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();
    config.SetInMemorySagaRepositoryProvider();

    var ass = typeof(Program).Assembly;

    config.AddConsumers(ass);
    config.AddSagaStateMachines(ass);
    config.AddSagas(ass);
    config.AddActivities(ass);

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("carrena");
            h.Password("carrena");
        });

        cfg.ConfigureEndpoints(ctx);

        cfg.PrefetchCount = 1; // bus control specific
        cfg.UseConcurrencyLimit(1);
        
    });
});


var host = builder.Build();
host.Run();
