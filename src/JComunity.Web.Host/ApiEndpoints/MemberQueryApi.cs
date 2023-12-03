using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace JComunity.Host.Web.ApiEndpoints;


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

        

        return Results.Ok(req);
    }
}
