using JCommunity.AppCore.Entities.MemberAggregate;

namespace JCommunity.Web.Contract.MemberApi.Validators;

public sealed class CreateMemberRequestValidator : AbstractValidator<CreateMemberRequest>
{
    private readonly IMemberRepository _memberRepository;
    public CreateMemberRequestValidator(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;

        RuleFor(r => r.name)
            .MinimumLength(MemberRestriction.NAME_MIN_LENGTH)
            .MaximumLength(MemberRestriction.NAME_MAX_LENGTH)
            .WithMessage($"Check the length in the [Name] string. (Min:{MemberRestriction.NAME_MIN_LENGTH} , Max:{MemberRestriction.NAME_MAX_LENGTH})");

        RuleFor(r => r.nickName)
            .MinimumLength(MemberRestriction.NICKNAME_MIN_LENGTH)
            .MaximumLength(MemberRestriction.NICKNAME_MAX_LENGTH)
            .WithMessage($"Check the length in the [NickName] string. (Min:{MemberRestriction.NICKNAME_MIN_LENGTH} , Max:{MemberRestriction.NICKNAME_MAX_LENGTH})");

        RuleFor(r => r.nickName)
            .MustAsync(async (nickName, _) =>
            {
                return await _memberRepository.IsUniqueNickName(nickName, _);
            })
            .WithMessage("NickName must be unique.");
            

        RuleFor(r => r.email)
            .MaximumLength(MemberRestriction.EMAIL_MAX_LENGTH)
            .WithMessage($"Check the length in the [Email] string. (Max:{MemberRestriction.NAME_MAX_LENGTH})");

        RuleFor(r => r.email)
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(r => r.email)
          .MustAsync(async (email, _) =>
          {
              return await _memberRepository.IsUniqueEmail(email, _);
          })
          .WithMessage("NickName must be unique.");


        RuleFor(r => r.password)
            .MinimumLength(MemberRestriction.PASSWORD_MIN_LENGTH)
            .MaximumLength(MemberRestriction.PASSWORD_MAX_LENGTH)
            .WithMessage($"Check the length in the [Password] string. (Min:{MemberRestriction.PASSWORD_MIN_LENGTH} , Max:{MemberRestriction.PASSWORD_MAX_LENGTH})");

        //RuleFor(r => r.memberStatus)
        //    .Must(status =>
        //    {
        //        if (status > -1 && status < 4) return true;

        //        return false;
        //    })
        //    .WithMessage("Member status values must be between 1 and 4");


    }
}


//CreateMemberRequest(string name, string nickName, string email, string password, int memberStatus)
