namespace JCommunity.Services.Extentions;

public static class Extentions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        #region [Validator]
        builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
        #endregion

        #region [Repository]
        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        builder.Services.AddScoped<ITopicRepository, TopicRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();

        builder.Services.AddSingleton<IFileRepository, FileRepository>();
        builder.Services.AddSingleton<IFileService, FileService.FileService>();
        #endregion

        #region [MediatR]
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(AssemblyReference));

        }).AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        #endregion

        #region [AutoMapper]
        builder.Services
            .AddAutoMapper(AssemblyReference.Assembly);
        #endregion
    }

}

