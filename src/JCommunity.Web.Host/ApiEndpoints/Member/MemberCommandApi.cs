namespace JCommunity.Web.Host.ApiEndpoints.Member;

public static class MemberCommandApi
{
    public static IEndpointRouteBuilder MapMemberCommandApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", CreateMember);

        return app;
    }

    public static async Task<IResult> CreateMember(
        [FromBody] CreateMemberCommand command,
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(command, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

}
