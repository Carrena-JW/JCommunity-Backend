namespace JCommunity.Services.MemberService.Queries;

public class GetTopicTags
{
    public record Query : IQuery<IEnumerable<TopicTag>>
    {
        #region [Query Parameters]
        public string TopicId { get; init; } = string.Empty;

        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Query, Result<IEnumerable<TopicTag>>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ITopicRepository _repository;

            public Handler(ILogger<Handler> logger, ITopicRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _repository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<IEnumerable<TopicTag>>> Handle(Query query, CancellationToken ct)
            {
                var options = TopicIncludeOptions.Create(true);
                var topic = await _repository.GetTopicByIdAsync(Guid.Parse(query.TopicId), options, ct);

                if (topic == null) return Result.Fail(new TopicError.NotFound(query.TopicId));

                var tags = topic.Tags.AsEnumerable();

                _logger.LogInformation("Get Topic Tags - tags: {@tags}", tags);

                return Result.Ok(tags);
            }
        }
        #endregion
    }
}