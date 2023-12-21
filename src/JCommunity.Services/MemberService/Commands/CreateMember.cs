namespace JCommunity.Services.MemberService.Commands;

public class CreateMember 
{
    public record Command : ICommand<Command, string> 
    {
        #region [Command Parameters Model]
        public string Name { get; init; } = string.Empty;
        public string NickName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Name)
                    .NotEmpty()
                    .MinimumLength(MemberRestriction.NAME_MIN_LENGTH)
                    .MaximumLength(MemberRestriction.NAME_MAX_LENGTH);

                RuleFor(r => r.NickName)
                    .NotEmpty()
                    .MinimumLength(MemberRestriction.NICKNAME_MIN_LENGTH)
                    .MaximumLength(MemberRestriction.NICKNAME_MAX_LENGTH);

                RuleFor(r => r.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MaximumLength(MemberRestriction.EMAIL_MAX_LENGTH);


                RuleFor(r => r.Password)
                    .NotEmpty()
                    .MinimumLength(MemberRestriction.PASSWORD_MIN_LENGTH)
                    .MaximumLength(MemberRestriction.PASSWORD_MAX_LENGTH);
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMemberRepository _memberRepository;

            public Handler(IMemberRepository memberRepository, ILogger<Handler> logger)
            {
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken ct)
            {
                var member = Member.Create(command.Name, command.NickName, command.Password, command.Email);
                
                //check if nickname and email is the unique value
                var isUniqueEmail =  await _memberRepository.IsUniqueEmailAsync(command.Email, ct);
                if(isUniqueEmail == false) return Result.Fail(new MemberError.EmailNotUnique(command.Email));

                var IsUniqueNickNameAsync = await _memberRepository.IsUniqueNickNameAsync(command.NickName, ct);
                if(IsUniqueNickNameAsync == false) return Result.Fail(new MemberError.NicknameNotUnique(command.NickName));

                var result = _memberRepository.Add(member);
                await _memberRepository.UnitOfWork.SaveEntitiesAsync(ct);

                _logger.LogInformation("Creating Member - member: {@member}", member);
                return result.GetMemberId();
            }
        }
        #endregion
    }
}

 