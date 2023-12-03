namespace JComunity.Web.Host.ApiEndpoints;

public static class MemberCommandApi
{
    public static IEndpointRouteBuilder MapMemberCommandApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", () => Results.Ok());
        app.MapPost("/someRequest", SomePostAction);

        return app;
    }

    public static async Task<IResult> SomePostAction(
        [FromBody] SomeRequest req,
        IValidator<SomeRequest> validator,
        [AsParameters] MemberApiService services)
    {
        var validate = await validator.ValidateAsync(req);
        if (!validate.IsValid) return Results.ValidationProblem(validate.ToDictionary());

        return Results.Ok();
    }

}
