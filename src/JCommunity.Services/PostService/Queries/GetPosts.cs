namespace JCommunity.Services.PostService.Queries;

public class GetPosts
{
	public record Query : IQuery<IEnumerable<Post>>
	{
        #region [Query Parameters]
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
        internal sealed class Handler : IRequestHandler<Query, Result<IEnumerable<Post>>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;

            public Handler(ILogger<Handler> logger, IPostRepository postRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentException(nameof(PostReport));
            }


            public async Task<Result<IEnumerable<Post>>> Handle(Query query, CancellationToken token)
            {
                var options = PostIncludeOptions.Build()
                    .BindLikeOption(query.IncludeLike)
                    .BindAuthorOption(query.IncludeAuthor)
                    .BindCommentsOption(query.IncludeComments)
                    .BindContentsOption(query.IncludeContents)
                    .BindReportsOption(query.IncludeReports)
                    .BindTopicOption(query.IncludeTopic);

                var posts = await _postRepository.GetPostsAsync(options,token);

                _logger.LogInformation("Get Posts - posts: {@posts}", posts);
                return Result.Ok(posts);
                 
            }
        }
        #endregion
    }

}

