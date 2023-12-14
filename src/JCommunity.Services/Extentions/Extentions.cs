namespace JCommunity.Services.Extentions;

public static class Extentions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        builder.Services.AddScoped<ITopicRepository, TopicRepository>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(AssemblyReference));

        }).AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
 
}
