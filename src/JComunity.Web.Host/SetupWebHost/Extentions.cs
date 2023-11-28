using JComunity.Host.Web.ApiEndpoints;
using System.Xml.Linq;

namespace JComunity.Web.Host.SetupHost
{
    public static class Extentions
    {
        public static IServiceCollection AddWebHostServices(
            this IServiceCollection services,
            IConfiguration config, 
            IWebHostEnvironment env)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IApplicationBuilder UseWebHostApplications(
            this WebApplication app, 
            IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGroup("/api/v1/Member")
                .WithTags(new[] { "Member Query API" })
                .MapMemberQueryApi();

            app.MapGroup("/api/v1/Member")
                .WithTags(new[] { "Member Command API" })
                .MapMemberCommandApi();


            return app;
        }
    }
}
