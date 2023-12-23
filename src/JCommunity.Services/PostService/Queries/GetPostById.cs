namespace JCommunity.Services.PostService.Queries;

public class GetPostById
{
	public record Query : IQuery<Post>
	{
        #region [Query Parameters]
        public string Id { get; init; } = string.Empty;
        //Include Options
        public bool? IncludeAuthor { get; init; } = null;
        public bool? IncludeTopic { get; init; } = null;
        public bool? IncludeLike { get; init; } = null;
        public bool? IncludeComments { get; init; } = null;
        public bool? IncludeReports { get; init; } = null;
        public bool? IncludeContents { get; init; } = null;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal sealed class Handler : IRequestHandler<Query, Result<Post>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;

            public Handler(ILogger<Handler> logger, IPostRepository postRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentException(nameof(PostReport));
            }


            public async Task<Result<Post>> Handle(Query query, CancellationToken token)
            {
                var options = PostIncludeOptions.Build()
                    .BindLikeOption(query.IncludeLike)
                    .BindAuthorOption(query.IncludeAuthor)
                    .BindCommentsOption(query.IncludeComments)
                    .BindContentsOption(query.IncludeContents)
                    .BindReportsOption(query.IncludeReports)
                    .BindTopicOption(query.IncludeTopic);

                var post = await _postRepository
                    .GetPostByIdAsync(query.Id.ConvertToUlid(), options, token);

                if (post == null) return Result.Fail(new PostError.NotFound(query.Id));

                _logger.LogInformation("Get Post By Id - post: {@post}", post);
                return post;
            }
        }
        #endregion
    }

}

