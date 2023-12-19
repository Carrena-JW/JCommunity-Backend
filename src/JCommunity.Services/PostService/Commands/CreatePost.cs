namespace JCommunity.Services.PostService.Commands;

public class CreatePost
{
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model]
        [JsonIgnore]
        public IFormFile Image { get; init; } = null!;
        public string TopicId { get; init; } = string.Empty;
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
                RuleFor(x => x.Image)
                    .NotNull()
                    .Must(image => image.IsImage());

                RuleFor(x => x.Image)
                    .Must(image => image.Length < 10485760)
                    .WithMessage("Image files cannot be more than 10MB.");
               

                RuleFor(x => x.Title)
                    .MaximumLength(PostRestriction.TITLE_MAX_LENGTH);
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
                var filePath = await _fileService.SaveFileAsync(command.Image, true, token);

                
                var baseUri = new Uri("http://localhost:5149");
                var attachment = PostContentAttachment
                    .Create(command.Image.FileName, filePath, baseUri, command.Image.Length);
                
                
                // #03. Create post entity
                var post = Post.Create(
                    command.TopicId.ConvertToGuid(),
                    command.Title,
                    command.HtmlBody,
                    command.Sources,
                    command.AuthorId.ConvertToGuid(),
                    attachment);

                // Set Draf = false
                post.SetFinished();

                // #04. Save to dbcontext
                _postRepository.Add(post);
                await _postRepository.UnitOfWork.SaveChangesAsync(token);


                // #05. return created post id
                _logger.LogInformation("Creating Post - post: {@post}", post);
                return post.Id.ToString();
            }
        }
        #endregion

    }
}

 