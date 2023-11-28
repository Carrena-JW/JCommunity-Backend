using JComunity.Web.Host.SetupHost;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebHostServices(builder.Configuration, builder.Environment);

var app = builder.Build();
app.UseWebHostApplications(builder.Environment);

app.Run();
