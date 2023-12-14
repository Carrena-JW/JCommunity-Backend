namespace JCommunity.Services.MemberService.Queries;

public class GetTopics
{
    public record Query : IQuery<IEnumerable<Topic>>
    {
        #region [Query Parameters]
        public bool? IncludeAuthor { get; init; } 
        public bool? IncludeTags { get; init; } 
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Query, Result<IEnumerable<Topic>>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ITopicRepository _repository;

            public Handler(ILogger<Handler> logger, ITopicRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _repository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<IEnumerable<Topic>>> Handle(Query query, CancellationToken ct)
            {
                var options = TopicIncludeOptions
                   .Create(query.IncludeAuthor, query.IncludeTags);

                var topics = await _repository.GetAllTopicsAsync(options, ct);

                _logger.LogInformation("Get Topics - topics: {@topics}", topics);

                return Result.Ok(topics);
            }
        }
        #endregion
    }
}
