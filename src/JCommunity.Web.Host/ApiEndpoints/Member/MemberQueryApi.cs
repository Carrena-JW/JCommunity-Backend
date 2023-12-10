namespace JCommunity.Web.Host.ApiEndpoints.Member;

internal static class MemberQueryApi
{

    internal static IEndpointRouteBuilder MapMemberQueryApi(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapGet("/", GetMembersAsync);
        app.MapGet("/{id}", GetMemberByIdAsync);
        #endregion

        return app;
    }

    private static async Task<IResult> GetMemberByIdAsync(
       [AsParameters] GetMemberById.Query request,
       [AsParameters] MemberApiService services,
       CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetMembersAsync(
        [AsParameters] GetMembers.Query request,
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return Results.Ok(result.Value);
    }
 

}
