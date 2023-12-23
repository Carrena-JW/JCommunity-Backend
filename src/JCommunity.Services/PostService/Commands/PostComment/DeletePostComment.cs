namespace JCommunity.Services.PostService.Commands;

public class DeletePostComment
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model]
        public string PostId { get; init; } = string.Empty;
        public string PostCommentId { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;

            public Handler(
                ILogger<Handler> logger, 
                IPostRepository postRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            }

            public async Task<Result<bool>> Handle(Command command, CancellationToken token)
            {

                // #01. Find Posting
                var options = PostIncludeOptions.Build()
                    .BindCommentsOption(true);

                var postId = command.PostId.ConvertToUlid();
                var post = await _postRepository
                    .GetPostByIdAsync(postId, options, token);
                
                if(post == null)
                {
                    return Result.Fail(new  PostError.NotFound(command.PostId));
                }

                // #02. Remove Comment
                var postCommentId = command.PostCommentId.ConvertToUlid();
                post.RemoveComment(postCommentId);
                   
                // #03. Save Entity
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);


                // #05. return created post id
                _logger.LogInformation("Deleting Post Comment - comment: {@postCommentId}", postCommentId);
                return true;
            }
        }
        #endregion

    }
}

 