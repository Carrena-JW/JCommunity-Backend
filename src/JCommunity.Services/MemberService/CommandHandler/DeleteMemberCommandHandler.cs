using JCommunity.AppCore.Core.Errors;

namespace JCommunity.Services.MemberService.CommandHandler;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Result<bool>>
{
    private readonly ILogger<DeleteMemberCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public DeleteMemberCommandHandler(
        ILogger<DeleteMemberCommandHandler> logger, 
        IMemberRepository memberRepository
        )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));

    }

    public async Task<Result<bool>> Handle(DeleteMemberCommand command, CancellationToken ct)
    {
        var findMember = await _memberRepository.GetByIdAsync(Member.CreateId(command.id),ct);

        if(findMember == null) return Result.Fail(new MemberError.NotFound(command.id));
        
        _logger.LogInformation("Deleting Member - member: {@member}", findMember);
        _memberRepository.DeleteMember(findMember);

        await _memberRepository.UnitOfWork.SaveChangesAsync(ct);

        return true;

    }
     
}
