using JCommunity.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace JCommunity.Services.Extentions;

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
