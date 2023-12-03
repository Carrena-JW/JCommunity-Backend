using JComunity.AppCore.Entities.MemberAggregate;
using JComunity.Services.MemberService.Command;

namespace JComunity.Web.Host.ApiEndpoints.Member;

public static class MemberCommandApi
{
    public static IEndpointRouteBuilder MapMemberCommandApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", CreateMember);
        app.MapPost("/someRequest", SomePostAction);

        return app;
    }

    public static async Task<IResult> CreateMember(
        [FromBody] CreateMemberRequest req,
        IValidator<CreateMemberRequest> validator,
        [AsParameters] MemberApiService services)
    {
        var validate = await validator.ValidateAsync(req);
        if (!validate.IsValid) return Results.ValidationProblem(validate.ToDictionary());

        var command = new CreateMemberCommand(
            req.name,
            req.nickName,
            req.email,
            req.password);


        var commandResult = await services.Mediator.Send(command);

        return Results.Ok(commandResult);
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
