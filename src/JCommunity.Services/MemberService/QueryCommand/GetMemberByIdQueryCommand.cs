namespace JCommunity.Services.MemberService.QueryCommand;

public record GetMemberByIdQueryCommand(string id) : IQuery<Result<MemberDto>>;
