namespace JComunity.Host.Web.ApiEndpoints;

public static class MemberQueryApi
{
    public static IEndpointRouteBuilder MapMemberQueryApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok("data"));
        return app;
    }
}
