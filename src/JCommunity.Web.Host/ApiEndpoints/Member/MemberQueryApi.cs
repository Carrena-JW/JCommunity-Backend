

namespace JCommunity.Web.Host.ApiEndpoints.Member;

public static class MemberQueryApi
{

    internal static IEndpointRouteBuilder MapMemberQueryApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetMembersAsync);
        app.MapGet("/{id}", GetMemberByIdAsync);
        app.MapGet("/someGetAction", SomeGetAction);
        return app;
    }
    private static async Task<IResult> GetMembersAsync([AsParameters] MemberApiService services)
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetMemberByIdAsync(
        [AsParameters] GetMemberByIdRequest req,
        [AsParameters] MemberApiService services)
    {
        var queryCommand = new GetMemberByIdQueryCommand(req.id);
        var result = await services.Mediator.Send(queryCommand);

        return Results.Ok(result);
    }

    private static async Task<IResult> SomeGetAction(
        [AsParameters] SomeRequest req,
        IValidator<SomeRequest> validator,
        [AsParameters] MemberApiService services
        )
    {
        var result = await services.Mediator.Send(req);
        return Results.Ok(req);
    }
}
