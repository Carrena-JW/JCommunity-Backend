namespace JCommunity.Services.MemberService.Queries;

public class GetMemberById
{
    public record Query : IQuery<MemberDto>
    {
        #region [Query Parameters]
        public string Id { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Query, Result<MemberDto>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMemberRepository _memberRepository;

            public Handler(ILogger<Handler> logger, IMemberRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<MemberDto>> Handle(Query request, CancellationToken ct)
            {

                var member = await _memberRepository.GetByIdAsync(Member.ConvertMemberIdFromString(request.Id), ct);
                _logger.LogInformation("Get Members - member: {@member}", member);
                if (member == null) return Result.Ok();

                return new MemberDto(member.Id.id.ToString(), member.Name, member.Email, member.NickName);
            }
        }
        #endregion
    }
}
