namespace JCommunity.Services.PostService.Commands;

public class SetPostLike
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model]
        public string PostId { get; init; } = string.Empty;
        public bool IsLike { get; init; }
        public string AuthorId { get; set; } = string.Empty;
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

            public async Task<Result<bool>> Handle(Command command, CancellationToken token)
            {
                // #01. Check post
                var options = PostIncludeOptions.Build()
                    .BindLikeOption(true);

                var postId = command.PostId.ConvertToUlid();

                var post = await _postRepository.GetPostByIdAsync(postId, options, token);

                if (post == null)
                {
                    return Result.Fail(new PostError.NotFound(postId.ToString()));
                }

                // #03. Check Author
                var memberId = command.AuthorId.ConvertToUlid();
                var isExistsMember = await _memberRepository.IsExistsMemberAsync(memberId, token);

                if (!isExistsMember)
                {
                    return Result.Fail(new MemberError.NotFound(memberId.ToString()));
                }

                // #04. Create Or Update Post Comment Like
                var like = post.CreateUpdatePostLike(memberId, command.IsLike);

                // #04. Save Entity
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);

                _logger.LogInformation("Set Post Like - like: {@like}", like);
                return true;
            }
        }
        #endregion

    }
}

 