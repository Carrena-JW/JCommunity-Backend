namespace JComunity.Services.MemberService.Command;

public sealed record CreateMemberCommand(
    string name, 
    string nickName, 
    string email, 
    string password) : IRequest<string>;
