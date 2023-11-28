namespace JComunity.Host.Web.ApiEndpoints
{
    public static class MemberCommandApi
    {
        public static IEndpointRouteBuilder MapMemberCommandApi(this IEndpointRouteBuilder app)
        {
            app.MapPost("/", ()=> Results.Ok());
            return app;
        }
    }
}
