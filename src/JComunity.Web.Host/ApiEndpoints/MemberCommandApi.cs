using FluentValidation;
using JComunity.Web.Contract.MemberApi.Requests;
using JComunity.Web.Host.ApiEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JComunity.Host.Web.ApiEndpoints
{
    public static class MemberCommandApi
    {
        public static IEndpointRouteBuilder MapMemberCommandApi(this IEndpointRouteBuilder app)
        {
            app.MapPost("/", () => Results.Ok());

            app.MapPost("/someRequest", SomePostAction);


            return app;
        }

        public static async Task<IResult> SomePostAction(
            SomeRequest req,
            IValidator<SomeRequest> validator,
            [AsParameters] MemberApiService services)
        {
            var validate = await validator.ValidateAsync(req);
            if (!validate.IsValid) return TypedResults.ValidationProblem(validate.ToDictionary());
             

            return TypedResults.Ok();
        }

    }
}
