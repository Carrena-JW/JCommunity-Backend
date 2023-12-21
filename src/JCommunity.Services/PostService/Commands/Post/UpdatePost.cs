namespace JCommunity.Services.PostService.Commands;

public class UpdatePost
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model]
        [JsonIgnore]
        public IFormFile? Image { get; init; } = null!;
        public string PostId { get; init; } = string.Empty;
        public string? TopicId { get; init; } = string.Empty;
        public string? Title { get; init; } 
        public string? Sources { get; init; }
        public string? HtmlBody { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Image)
                    .Must(image => image != null ? image.IsImage() : true);

                RuleFor(x => x.Image)
                    .Must(image => image != null ? image.Length < 10485760 : true)
                    .WithMessage("Image files cannot be more than 10MB.");

                RuleFor(x => x.Title)
                    .MinimumLength(PostRestriction.TITLE_MIN_LENGTH)
                    .MaximumLength(PostRestriction.TITLE_MAX_LENGTH);

                RuleFor(x => x.Sources)
                    .MaximumLength(PostRestriction.SOURCES_MAX_LENGTH);
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;
            private readonly IFileService _fileService;
            private readonly ITopicRepository _topicRepository;

            public Handler(
                ILogger<Handler> logger,
                IPostRepository postRepository,
                IFileService fileService,
                ITopicRepository topicRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
                _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
                _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));
            }

            public async Task<Result<bool>> Handle(Command command, CancellationToken token)
            {

                // #01. Find Post Entity
                var options = PostIncludeOptions.Build()
                    .BindContentsOption(true);

                var postId = command.PostId.ConvertToGuid();
                var post = await _postRepository.GetPostByIdAsync(postId, options, token);

                if (post == null)
                {
                    return Result.Fail(new PostError.NotFound(postId.ToString()));
                }

                // #02. Check isExists Topic && Update Topic 
                if (command.TopicId != null)
                {
                    var isExistsTopic = await _topicRepository
                    .IsExistsTopicAsync(command.TopicId.ConvertToGuid(), token);

                    if (!isExistsTopic)
                    {
                        return Result.Fail(new TopicError.NotFound(command.TopicId));
                    }

                    post.UpdateTopic(command.TopicId.ConvertToGuid());
                }
                 

                // #03. Update Image File
                if (command.Image != null) {
                    if (!command.Image.IsImage())
                    {
                        return Result.Fail(new PostError.NotImage(command.Image));
                    }

                    // ##. Save image file
                    var filePath = await _fileService.SaveFileAsync(command.Image, true, token);


                    var baseUri = new Uri("http://localhost:5149");
                    var attachment = PostContentAttachment
                        .Create(command.Image.FileName, filePath, baseUri, command.Image.Length);

                    post.Contents.UpdateMainImage(attachment);
                }

                // #04. Update Title
                if(command.Title != null)
                {
                    post.UpdateTitle(command.Title);
                }

                // #05. Update sources
                if (command.Sources != null)
                {
                    post.UpdateSources(command.Sources);
                }

                // #06. update body
                if (command.HtmlBody != null)
                {
                    post.Contents.UpdateHtmlBody(command.HtmlBody);
                }

                post.UpdateLastUpdateAt();

                // #07. Save to dbcontext
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);

                _logger.LogInformation("Updating Post - post: {@post}", post);
                return true;
            }
        }
        #endregion

    }
}

 