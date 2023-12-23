namespace JCommunity.Services.PostService.Commands;

public class CreatePostComment
{
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model]
        public string? ParentCommentId { get; init; }
        public string PostId { get; init; } = string.Empty;
        public string Contents { get; init; } = string.Empty;
        public string AuthorId { get; init; } = string.Empty;
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
        internal class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;
            private readonly IMemberRepository _memberRepository;

            public Handler(
                ILogger<Handler> logger, 
                IPostRepository postRepository, 
                IMemberRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken token)
            {
                // #00. Find Member by AuthorId
                var IsExistsMemberAsync = await _memberRepository
                    .IsExistsMemberAsync(command.AuthorId.ConvertToUlid(), token);

                if(!IsExistsMemberAsync)
                {
                    return Result.Fail(new MemberError.NotFound(command.AuthorId));
                }

                // #01. Find Posting
                var options = PostIncludeOptions.Build()
                    .BindCommentsOption(true);

                var post = await _postRepository
                    .GetPostByIdAsync(command.PostId.ConvertToUlid(), options, token);
                
                if(post == null)
                {
                    return Result.Fail(new  PostError.NotFound(command.PostId));
                }

                // #02. Create Posting Entity
                Ulid? parentCommentId = !string.IsNullOrEmpty(command.ParentCommentId) ?
                    command.ParentCommentId.ConvertToUlid() : null;

                //if has parentCommentId check exists item
                if(parentCommentId != null)
                {
                   var isExistsComment = post.IsExistsComment(parentCommentId.Value);
                   if(!isExistsComment) 
                   {
                        return Result.Fail(new PostError.CommentNotFound(parentCommentId.Value.ToString()));
                   }
                }

                // #03. Add Comment to Post
                var addedComment =  post.AddComment(command.Contents, command.AuthorId.ConvertToUlid(), parentCommentId);

                // #04. Save Entity
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);

                // #05. return created post id
                _logger.LogInformation("Adding Post Comment - comment: {@addedComment}", addedComment);
                return addedComment.Id.ToString();
            }
        }
        #endregion

    }
}

 