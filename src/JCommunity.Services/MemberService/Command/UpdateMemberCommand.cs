using System.Runtime.InteropServices;

namespace JCommunity.Services.MemberService.Command;

public sealed record UpdateMemberCommand(
    string id,
    [Optional] string nickName,
    [Optional] string email, 
    [Optional] string password) : ICommand<Result<bool>>;
