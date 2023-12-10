using JCommunity.AppCore.Core.Errors;

namespace JCommunity.Services.MemberService.CommandHandler;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Result<bool>>
{
    private readonly ILogger<UpdateMemberCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public UpdateMemberCommandHandler(
        ILogger<UpdateMemberCommandHandler> logger, 
        IMemberRepository memberRepository
        )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));

    }

    public async Task<Result<bool>> Handle(UpdateMemberCommand command, CancellationToken ct)
    {

        var findMember = await _memberRepository.GetByIdAsync(Member.CreateId(command.id), ct);

        if (findMember == null) return Result.Fail(new MemberError.NotFound(command.id));


        if (command.nickName != null) findMember.SetNickName(command.nickName);
        if (command.email != null) findMember.SetEmail(command.email);
        if (command.password != null) findMember.SetPassword(command.password);

        findMember.UpdateLastUpdateAt();

        _logger.LogInformation("Updating Member - member: {@member}", findMember);
        
        await _memberRepository.UnitOfWork.SaveChangesAsync(ct);
        

        return true;

    }
     
}
