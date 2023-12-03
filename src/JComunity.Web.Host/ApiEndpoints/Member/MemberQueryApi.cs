namespace JComunity.Web.Host.ApiEndpoints.Member;

public static class MemberQueryApi
{

    public static IEndpointRouteBuilder MapMemberQueryApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok("data"));
        app.MapGet("/someGetAction", SomeGetAction);
        return app;
    }

    public static async Task<IResult> SomeGetAction(
        [AsParameters] SomeRequest req,
        IValidator<SomeRequest> validator,
        [AsParameters] MemberApiService services
        )
    {
        var result = await services.Mediator.Send(req);
        return Results.Ok(req);
    }
}
