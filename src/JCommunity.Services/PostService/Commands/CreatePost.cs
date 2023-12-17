using System.Text.Json.Serialization;

namespace JCommunity.Services.PostService.Commands;

public class CreatePost
{
   
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model]
        [JsonIgnore]
        public IFormFile Image { get; init; } = null!;
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

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await Task.FromResult("Dddddd");
            }
        }
        #endregion
    }
}

 