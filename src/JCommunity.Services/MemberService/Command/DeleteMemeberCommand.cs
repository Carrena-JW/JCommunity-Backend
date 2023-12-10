namespace JCommunity.Services.MemberService.Command;

public sealed record DeleteMemberCommand(string id) : ICommand<Result<bool>>;
