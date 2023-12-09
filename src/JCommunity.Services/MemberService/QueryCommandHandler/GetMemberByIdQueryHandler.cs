namespace JCommunity.Services.MemberService.QueryCommandHandler;

public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQueryCommand, Result<MemberDto>>
{
    private readonly ILogger<GetMemberByIdQueryHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(
        ILogger<GetMemberByIdQueryHandler> logger, 
        IMemberRepository memberRepository)
    {
        _logger = logger;
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberDto>> Handle(GetMemberByIdQueryCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(new MemberId(Guid.Parse( request.id)),cancellationToken);
        _logger.LogInformation("Get Members - member: {@member}", member);
        if (member == null) return Result.Ok();

        return new MemberDto(member.Id.id.ToString(), member.Name, member.Email, member.NickName); 
    }
}
