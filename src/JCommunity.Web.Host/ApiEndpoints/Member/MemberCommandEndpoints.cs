namespace JCommunity.Web.Host.ApiEndpoints.Member;

internal static class MemberCommandEndpoints 
{
    private static readonly string API_ROOT = "/api/v1/Members";
    private static readonly string[] API_TAG = { "#02. Member Command API" };

    internal static IEndpointRouteBuilder ConfigureMemberCommandEndpoints(this RouteGroupBuilder app)
    {
        app.MapGroup(API_ROOT)
           .WithTags(API_TAG)
           .MapMemberCommandEndpoints();

        return app;
    }
    private static IEndpointRouteBuilder MapMemberCommandEndpoints(this IEndpointRouteBuilder app)
    {
        #region [Map Path]
        app.MapPost("/", CreateMemberAsync);
        app.MapPut("/", UpdateMemberAsync);
        app.MapDelete("/{id}", DeleteMemberAsync);
        #endregion
        
        return app;
    }

    private static async Task<IResult> CreateMemberAsync(
       [FromBody] CreateMember.Command reqeuest,
       [AsParameters] MemberApiService services,
       CancellationToken token = new())
    {
        var result = await services.Mediator.Send(reqeuest, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }


    private static async Task<IResult> UpdateMemberAsync(
        [FromBody] UpdateMember.Command reqeuest,
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(reqeuest, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }
     

    private static async Task<IResult> DeleteMemberAsync(
        [AsParameters] DeleteMember.Command reqeuest,
        [AsParameters] MemberApiService services,
        CancellationToken token = new())
    {
        var result = await services.Mediator.Send(reqeuest, token);

        return result.IsSuccess ? Results.Ok(result.Value) :
                                  Results.BadRequest(result.Errors);
    }

}
