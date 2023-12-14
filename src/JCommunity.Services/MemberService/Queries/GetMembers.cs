namespace JCommunity.Services.MemberService.Queries;

public class GetMembers
{
    public record Query : IQuery<IEnumerable<MemberDto>>
    {
        #region [Query Parameters]
        //has no parameters
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Query, Result<IEnumerable<MemberDto>>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IMemberRepository _memberRepository;

            public Handler(ILogger<Handler> logger, IMemberRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<IEnumerable<MemberDto>>> Handle(Query request, CancellationToken ct)
            {

                var members = await _memberRepository.GetAllMembersAsync(ct);
                _logger.LogInformation("Get Members - member: {@member}", members);
                var result = members.Select(x => new MemberDto(x.Id.ToString(), x.Name, x.Email, x.NickName));
                return Result.Ok(result);
            }
        }
        #endregion
    }
}
