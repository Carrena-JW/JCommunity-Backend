using JCommunity.AppCore.Entities.Member;

namespace JCommunity.Services.MemberService.QueryCommandHandler;

public class GetMembersQueryHandler : IRequestHandler<GetMembersQueryCommand, Result<IEnumerable<MemberDto>>>
{
    private readonly ILogger<GetMembersQueryHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public GetMembersQueryHandler(
        ILogger<GetMembersQueryHandler> logger, 
        IMemberRepository memberRepository)
    {
        _logger = logger;
        _memberRepository = memberRepository;
    }

  

    public async Task<Result<IEnumerable<MemberDto>>> Handle(GetMembersQueryCommand request, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetAllMembersAsync(cancellationToken);
        _logger.LogInformation("Get Members - member: {@member}", members);
        var result = members.Select(x => new MemberDto(x.Id.id.ToString(), x.Name, x.Email, x.NickName));
        return Result.Ok(result);
    }
}
