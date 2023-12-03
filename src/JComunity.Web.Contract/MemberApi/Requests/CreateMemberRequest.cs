namespace JComunity.Web.Contract.MemberApi.Requests;

public sealed record CreateMemberRequest(
    string name, 
    string nickName, 
    string email, 
    string password) : IRequestContract;

