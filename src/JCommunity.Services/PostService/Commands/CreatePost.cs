namespace JCommunity.Services.PostService.Commands;

public class CreatePost
{
   
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model]
        [JsonIgnore]
        public IFormFile Image { get; init; } = null!;
        public Guid TopicId { get; init; } 
        public string Title { get; init; } = string.Empty;
        public string Sources { get; init; } = string.Empty;
        public string AuthorId { get; init; } = string.Empty;
        public string HtmlBody { get; init; } = string.Empty;
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
        internal class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;
            private readonly IFileService _fileService;

            public Handler(ILogger<Handler> logger, IPostRepository postRepository, IFileService fileService)
            {
                _logger = logger;
                _postRepository = postRepository;
                _fileService = fileService;
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken token)
            {
                // #01. Check image file
                if (!command.Image.IsImage())
                {
                    return Result.Fail(new PostError.NotImage(command.Image));
                }

                // #02. Save image file
                await _fileService.SaveFileAsync(command.Image, true, token);

                // #03. Create post entity
                var post = Post.Create(
                    command.TopicId,
                    command.Title,
                    command.HtmlBody,
                    command.Sources,
                     command.AuthorId)

                // #04. Save to dbcontext

                // #05. return created post id

                return await Task.FromResult("Dddddd");
            }
        }
        #endregion
    }
}

 