namespace JCommunity.Services.MemberService.QueryCommand;

public record GetMemberByIdQueryCommand(string id) : IRequest<string>;
