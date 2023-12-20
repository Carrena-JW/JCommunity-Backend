namespace JCommunity.Services.PostService.Queries;

public class GetPostComments
{
	public record Query : IQuery<IEnumerable<PostComment>>
	{
        #region [Query Parameters]
        public string Id { get; init; } = string.Empty;


        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Query> { }
        #endregion

        #region [Handler]
        internal sealed class Handler : IRequestHandler<Query, Result<IEnumerable<PostComment>>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;

            public Handler(ILogger<Handler> logger, IPostRepository postRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentException(nameof(PostReport));
            }


            public async Task<Result<IEnumerable<PostComment>>> Handle(Query query, CancellationToken token)
            {
                var options = PostIncludeOptions.Build()
                    .BindCommentsOption(true);

                var post = await _postRepository.GetPostByIdAsync(query.Id.ConvertToGuid(), options, token);

                if (post == null) return Result.Fail(new PostError.NotFound(query.Id));



                _logger.LogInformation("Get Post Comments - comments: {@post.Comments}", post.Comments);
                return Result.Ok(post.Comments.AsEnumerable());
                 
            }
        }
        #endregion
    }

}

