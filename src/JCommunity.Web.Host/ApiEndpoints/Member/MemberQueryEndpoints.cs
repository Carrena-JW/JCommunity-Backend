namespace JCommunity.Web.Host.ApiEndpoints.Member;

internal static class MemberQueryEndpoints
{
    private static readonly string API_ROOT = "/api/v1/Members";
    private static readonly string[] API_TAG = { "#03. Member Query API" };

    internal static IEndpointRouteBuilder ConfigureMemberQueryEndpoints(this RouteGroupBuilder app)
    {
        app.MapGroup(API_ROOT)
            .WithTags(API_TAG)
            .MapMemberQueryEndpoints();

        return app;
    }

    private static IEndpointRouteBuilder MapMemberQueryEndpoints(this IEndpointRouteBuilder app)
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
        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

    private static async Task<IResult> GetMembersAsync(
        [AsParameters] GetMembers.Query request,
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(request, token);
        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }
 

}
