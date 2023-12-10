namespace JCommunity.Services.MemberService.Validators;

public sealed class UpdateMemberRequestValidator : AbstractValidator<UpdateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    public UpdateMemberRequestValidator(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;

        RuleFor(r => r.id)
            .NotEmpty()
            .NotNull();

        RuleFor(r => r.nickName)
            .MinimumLength(MemberRestriction.NICKNAME_MIN_LENGTH)
            .MaximumLength(MemberRestriction.NICKNAME_MAX_LENGTH);
            

        RuleFor(r => r.nickName)
            .MustAsync(async (nickName, _) =>
            {
                if (nickName == null) return await Task.FromResult(true);
                return await _memberRepository.IsUniqueNickName(nickName, _);
            })
            .WithMessage("NickName must be unique.");


        RuleFor(r => r.email)
            .MaximumLength(MemberRestriction.EMAIL_MAX_LENGTH);

        RuleFor(r => r.email)
            .EmailAddress();
          
        RuleFor(r => r.email)
          .MustAsync(async (email, _) =>
          {   if (email == null) return await Task.FromResult(true);
              return await _memberRepository.IsUniqueEmail(email, _);
          })
          .WithMessage("Email must be unique.");


        RuleFor(r => r.password)
            .MinimumLength(MemberRestriction.PASSWORD_MIN_LENGTH)
            .MaximumLength(MemberRestriction.PASSWORD_MAX_LENGTH);
           
               
    }
}


