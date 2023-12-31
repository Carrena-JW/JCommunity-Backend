﻿namespace JCommunity.Services.MemberService.Commands;

public class UpdateMember
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model]
        public string Id { get; init; } = string.Empty;
        public string? Nickname { get; init; } 
        public string? Email { get; init; } 
        public string? Password { get; init; }
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Id)
                    .NotEmpty()
                    .NotNull();

                RuleFor(r => r.Nickname)
                    .MinimumLength(MemberRestriction.NICKNAME_MIN_LENGTH)
                    .MaximumLength(MemberRestriction.NICKNAME_MAX_LENGTH);

                RuleFor(r => r.Email)
                    .EmailAddress()
                    .MaximumLength(MemberRestriction.EMAIL_MAX_LENGTH);

                RuleFor(r => r.Password)
                    .MinimumLength(MemberRestriction.PASSWORD_MIN_LENGTH)
                    .MaximumLength(MemberRestriction.PASSWORD_MAX_LENGTH);
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

                //check if nickname and email is the unique value
                if (!string.IsNullOrEmpty(command.Email) &&
                    findMember.Email != command.Email)
                {
                    var isUniqueEmail = await _memberRepository.IsUniqueEmailAsync(command.Email, ct);
                    if (isUniqueEmail == false) return Result.Fail(new MemberError.EmailNotUnique(command.Email));
                    findMember.UpdateEmail(command.Email);
                }

                if (!string.IsNullOrEmpty(command.Nickname) &&
                    findMember.NickName != command.Nickname)
                {
                    var IsUniqueNickNameAsync = await _memberRepository.IsUniqueNickNameAsync(command.Nickname, ct);
                    if (IsUniqueNickNameAsync == false) return Result.Fail(new MemberError.NicknameNotUnique(command.Nickname));
                    findMember.UpdateNickname(command.Nickname);
                }

                if(!string.IsNullOrEmpty(command.Password)) 
                {
                    findMember.UpdatePassword(command.Password);
                }

                findMember.UpdateLastUpdateAt();
                await _memberRepository.UnitOfWork.SaveEntitiesAsync(ct);
                
                _logger.LogInformation("Updating Member - member: {@member}", findMember);
                return true;
            }
        }
        #endregion
    }
}
