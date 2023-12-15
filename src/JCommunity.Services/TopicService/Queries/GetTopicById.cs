namespace JCommunity.Services.MemberService.Queries;

public class GetTopicById
{
    public record Query : IQuery<Topic>
    {
        #region [Query Parameters]
        public string Id { get; init; } = string.Empty;
        public bool? IncludeAuthor { get; init; } 
        public bool? IncludeTags { get; init; } 
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Query, Result<Topic>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ITopicRepository _repository;

            public Handler(ILogger<Handler> logger, ITopicRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _repository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<Topic>> Handle(Query query, CancellationToken ct)
            {
                var options = TopicIncludeOptions
                    .Create(query.IncludeAuthor, query.IncludeTags);
                 
                var topic = await _repository.GetTopicByIdAsync(Guid.Parse(query.Id), options,  ct);
                _logger.LogInformation("Get Topics - topics: {@topic}", topic);

                if (topic == null) return Result.Ok();

                return topic;
            }
        }
        #endregion
    }
}
