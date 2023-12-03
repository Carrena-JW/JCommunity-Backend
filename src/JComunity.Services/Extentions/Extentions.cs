using JComunity.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace JComunity.Services.Extentions;

public static class Extentions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {

        builder.Services.AddScoped<IMemberRepository, MemberRepository>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(AssemblyReference));

        });
    }
}
