namespace JCommunity.Web.Host.ApiEndpoints.Member;

internal static class MemberCommandApi
{
    public static IEndpointRouteBuilder MapMemberCommandApi(this IEndpointRouteBuilder app)
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
