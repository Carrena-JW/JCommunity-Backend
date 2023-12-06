using JCommunity.AppCore.Core.Models;

namespace JCommunity.Services.MemberService.CommandHandler;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<string>>
{
    private readonly ILogger<CreateMemberCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;
    private readonly IValidator<CreateMemberCommand> _validator;

    public CreateMemberCommandHandler(
        ILogger<CreateMemberCommandHandler> logger, 
        IMemberRepository memberRepository,
        IValidator<CreateMemberCommand> validator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    }

    public async Task<Result<string>> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        //validate command
        var validate =  await _validator.ValidateAsync(command);
         
        

        var member = Member.Create(command.name, command.nickName, command.password, command.email);

        _logger.LogInformation("Creating Member - member: {@member}", member);

        var result = _memberRepository.Add(member);
        await _memberRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Create(result.getMemberId());

    }
}
