namespace JCommunity.Services.MemberService.CommandHandler;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<string>>
{
    private readonly ILogger<CreateMemberCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(
        ILogger<CreateMemberCommandHandler> logger, 
        IMemberRepository memberRepository
        )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));

    }

    public async Task<Result<string>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
       
        var member = Member.Create(command.name, command.nickName, command.password, command.email);

        _logger.LogInformation("Creating Member - member: {@member}", member);

        var result = _memberRepository.Add(member);
        await _memberRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return result.getMemberId();

    }
     
}
