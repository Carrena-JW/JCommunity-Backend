namespace JCommunity.Services.PostService.Queries;

public class GetPosts
{
	public record Query : IQuery<IEnumerable<Post>>
	{
        #region [Query Parameters]
        // has no parameters
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


            public async Task<Result<IEnumerable<Post>>> Handle(Query request, CancellationToken token)
            {
                var posts = await _postRepository.GetPostsAsync(token);

                _logger.LogInformation("Get Posts - posts: {@posts}", posts);
                return Result.Ok(posts);
                 
            }
        }
        #endregion
    }

}

