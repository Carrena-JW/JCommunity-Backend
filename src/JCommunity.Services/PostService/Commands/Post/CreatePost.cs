using Microsoft.Extensions.Configuration;

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
                    .NotEmpty()
                    .MinimumLength(PostRestriction.TITLE_MIN_LENGTH)
                    .MaximumLength(PostRestriction.TITLE_MAX_LENGTH);

                RuleFor(x => x.Sources)
                 .MaximumLength(PostRestriction.SOURCES_MAX_LENGTH);
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IPostRepository _postRepository;
            private readonly IFileService _fileService;
            private readonly IMemberRepository _memberRepository;
            private readonly ITopicRepository _topicRepository;
            private readonly string BASE_URI;

            public Handler(
                ILogger<Handler> logger,
                IPostRepository postRepository,
                IFileService fileService,
                IMemberRepository memberRepository,
                ITopicRepository topicRepository,
                IConfiguration config)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
                _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
                _topicRepository = topicRepository ?? throw new ArgumentNullException(nameof(topicRepository));

                BASE_URI = config.GetValue<string>("BaseUri","http://localhost:5149")!;
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken token)
            {
                // #00. Find Member by AuthorId
                var IsExistsMemberAsync = await _memberRepository
                    .IsExistsMemberAsync(command.AuthorId.ConvertToUlid(), token);

                if (!IsExistsMemberAsync)
                {
                    return Result.Fail(new MemberError.NotFound(command.AuthorId));
                }

                // #01. Check image file
                if (!command.Image.IsImage())
                {
                    return Result.Fail(new PostError.NotImage(command.Image));
                }

                // #02. Save image file
                var filePath = await _fileService.SaveFileAsync(command.Image, true, token);
                var baseUri = new Uri(BASE_URI);

                // ## Check IsExists Topic
                var isExistsTopic = await _topicRepository.IsExistsTopicAsync(command.TopicId.ConvertToUlid(), token);

                if (!isExistsTopic)
                {
                    return Result.Fail(new TopicError.NotFound(command.TopicId));
                }

                
                // #03. Create post entity
                var post = Post.Create(
                    command.TopicId.ConvertToUlid(),
                    command.Title,
                    command.HtmlBody,
                    command.Sources,
                    command.AuthorId.ConvertToUlid(),
                    command.Image.FileName,
                    filePath,
                    command.Image.Length,
                    baseUri);

                // Set Draf = false
                post.SetFinished();

                // #04. Save to dbcontext
                _postRepository.Add(post);
                post.AddDomainEvent(new PostCreatedEvent(post.Id));
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);


                // #05. return created post id
                _logger.LogInformation("Creating Post - post: {@post}", post);
                return post.Id.ToString();
            }
        }
        #endregion

    }
}

 