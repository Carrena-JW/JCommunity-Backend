namespace JCommunity.Services.MemberService.QueryCommand;

public record GetMembersQueryCommand() : IQuery<Result<IEnumerable<MemberDto>>>;
