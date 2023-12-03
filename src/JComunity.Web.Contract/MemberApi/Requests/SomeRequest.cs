using JComunity.AppCore.Abstractions;

namespace JComunity.Web.Contract.MemberApi.Requests;


public sealed record SomeRequest(int? id, string? name) : IRequestContract;
