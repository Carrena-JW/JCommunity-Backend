namespace JCommunity.Services.PostService.Commands;

public class UpdatePostComment
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model]
        public string PostId { get; init; } = string.Empty;
        public string PostCommentId { get; init; } = string.Empty;
        public string Contents { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Contents)
                    .NotEmpty()
                    .MaximumLength(PostRestriction.COMMENT_CONTENTS_MAX_LENGTH);
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
                // #00. Find Member by AuthorId

                // #01. Find Posting
                var options = PostIncludeOptions.Build()
                    .BindCommentsOption(true);

                var postId = command.PostId.ConvertToGuid();
                var post = await _postRepository
                    .GetPostByIdAsync(postId, options, token);
                
                if(post == null)
                {
                    return Result.Fail(new  PostError.NotFound(command.PostId));
                }

                // #02. Find Post Comment
                var postCommentId = command.PostCommentId.ConvertToGuid();
                var postComment = post.GetPostCommentById(postCommentId);

                if(postComment == null)
                {
                    return Result.Fail(new PostError.CommentNotFound(command.PostCommentId));
                }

                // #03. Update Comment to Post
                postComment.UpdatePostCommentContents(command.Contents);

                // #04. Save Entity
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);


                // #05. return created post id
                _logger.LogInformation("Updating Post Comment - comment: {@postComment}", postComment);
                return true;
            }
        }
        #endregion

    }
}

 