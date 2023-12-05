using JCommunity.Domain.Entities.Users;
using JCommunity.Services.MemberService.Command;
using Microsoft.Extensions.Logging;

namespace JCommunity.Services.MemberService.CommandHandler;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, string>
{
    private ILogger<CreateMemberCommandHandler> _logger;
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(
        ILogger<CreateMemberCommandHandler> logger, 
        IMemberRepository memberRepository)
    {
        _logger = logger;
        _memberRepository = memberRepository;
    }

    public async Task<string> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var member = Member.Create(command.name, command.nickName, command.password, command.email);

        _logger.LogInformation("Creating Member - member: {@member}", member);

        var result = _memberRepository.Add(member);
        await _memberRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return result.Id.id.ToString();

    }
}
