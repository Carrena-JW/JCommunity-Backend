using FluentValidation;
using JCommunity.AppCore.Entities.Member;

namespace JCommunity.Services.MemberService.Validators;

public sealed class CreateMemberRequestValidator : AbstractValidator<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    public CreateMemberRequestValidator(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;

        RuleFor(r => r.name)
            .NotEmpty()
            .MinimumLength(MemberRestriction.NAME_MIN_LENGTH)
            .MaximumLength(MemberRestriction.NAME_MAX_LENGTH);

        RuleFor(r => r.nickName)
            .NotEmpty()
            .MinimumLength(MemberRestriction.NICKNAME_MIN_LENGTH)
            .MaximumLength(MemberRestriction.NICKNAME_MAX_LENGTH);
            

        RuleFor(r => r.nickName)
            .MustAsync(async (nickName, _) =>
            {
                return await _memberRepository.IsUniqueNickName(nickName, _);
            })
            .WithMessage("NickName must be unique.");


        RuleFor(r => r.email)
            .NotEmpty()
            .MaximumLength(MemberRestriction.EMAIL_MAX_LENGTH);


        RuleFor(r => r.email)
            .EmailAddress();
          
        RuleFor(r => r.email)
          .MustAsync(async (email, _) =>
          {
              return await _memberRepository.IsUniqueEmail(email, _);
          })
          .WithMessage("Email must be unique.");


        RuleFor(r => r.password)
            .NotEmpty()
            .MinimumLength(MemberRestriction.PASSWORD_MIN_LENGTH)
            .MaximumLength(MemberRestriction.PASSWORD_MAX_LENGTH);
           
               
    }
}


