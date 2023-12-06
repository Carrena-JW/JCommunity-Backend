

namespace JCommunity.Web.Host.ApiEndpoints.Member;

public static class MemberQueryApi
{

    internal static IEndpointRouteBuilder MapMemberQueryApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetMembersAsync);
        app.MapGet("/{id}", GetMemberByIdAsync);
        return app;
    }

    private static async Task<IResult> GetMembersAsync(
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        return Results.Ok();
    }

    private static async Task<IResult> GetMemberByIdAsync(
        [AsParameters] GetMemberByIdQueryCommand req,
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        return Results.Ok(await services.Mediator.Send(req, token));
    }

}
