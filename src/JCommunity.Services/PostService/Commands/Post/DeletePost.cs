namespace JCommunity.Services.PostService.Commands;

public class DeletePost
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model]
        public string Id { get; init; } = string.Empty;
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
                // #01. Find post 
                var options = PostIncludeOptions.Build();
                var postId = command.Id.ConvertToUlid();
                var post = await _postRepository.GetPostByIdAsync(postId, options, token);

                if(post == null)
                {
                    return Result.Fail(new PostError.NotFound(postId.ToString()));
                }

                // #02. Save to dbcontext
                _postRepository.Remove(post);
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);


              
                _logger.LogInformation("Deleting Post - post: {@post}", post);
                return true;
            }
        }
        #endregion

    }
}

 