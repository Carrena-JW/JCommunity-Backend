namespace JCommunity.Services.MemberService.Commands;

public class DeleteMember
{
    public record Command : ICommand<string, bool>
    {
        #region [Command Parameters Model]
        public string Id { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        internal class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Id)
                    .NotEmpty()
                    .NotNull();
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMemberRepository _memberRepository;

            public Handler(IMemberRepository memberRepository, ILogger<Handler> logger)
            {
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Result<bool>> Handle(Command command, CancellationToken ct)
            {
                var findMember = await _memberRepository.GetByIdAsync(command.Id.ConvertToUlid(), ct);

                if (findMember == null) return Result.Fail(new MemberError.NotFound(command.Id));

                _memberRepository.Remove(findMember);
                await _memberRepository.UnitOfWork.SaveEntitiesAsync(ct);

                _logger.LogInformation("Deleting Member - member: {@member}", findMember);
                return true;
            }
        }
        #endregion
    }
}
