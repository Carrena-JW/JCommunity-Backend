

namespace JComunity.Web.Contract.MemberApi.Validators;

public sealed class SomeValidator : AbstractValidator<SomeRequest>
{
    public SomeValidator()
    {
        RuleFor(request => request.name)
            .MustAsync(async (name, _) =>
            {
                await Task.Delay(1000);
                return false;
            })
            .WithMessage("name is must be unique");

        RuleFor(request => request.name)
            .MaximumLength(MemberRestriction.NICKNAME_MAX_LENGTH)
            .WithMessage($"Nickname must be no more than {MemberRestriction.NICKNAME_MAX_LENGTH} characters long.");
    }
}
