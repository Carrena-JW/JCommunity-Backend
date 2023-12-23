namespace JCommunity.Services.PostService.Commands;

public class CreatePostReport
{
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model]
        public string PostId { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public int ReportCategory { get; init; } = 0;
        public string HtmlBody { get; init; } = string.Empty;
        public string AuthorId { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Title)
                    .NotEmpty()
                    .MinimumLength(PostRestriction.REPORT_TITLE_MIN_LENGTH)
                    .MaximumLength(PostRestriction.REPORT_TITLE_MAX_LENGTH);

                //Category value 0~4
                RuleFor(x => x.ReportCategory)
                    .InclusiveBetween(0, 4)

                    .WithState(x => new
                    {
                        ReportCategoryValues = new
                        {
                            Etc = 0,
                            Sensational = 1,
                            Abusive = 2,
                            Disgust = 3,
                            Political = 4
                        }
                    });

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
                // #01. find post
                var options = PostIncludeOptions.Build()
                    .BindReportsOption(true);

                var postId = command.PostId.ConvertToUlid();
                var post = await _postRepository.GetPostByIdAsync(postId, options, token);

                if(post == null)
                {
                    return Result.Fail(new PostError.NotFound(postId.ToString()));
                }

                // #02. Check member
                var memberId = command.AuthorId.ConvertToUlid();
                var isExistsMember = await _memberRepository.IsExistsMemberAsync(memberId, token);

                if (!isExistsMember)
                {
                    return Result.Fail(new MemberError.NotFound(memberId.ToString()));
                }

                // #03. Is it a duplicated report?
                var isDuplicated = post.IsDuplicatedReport(memberId);

                if (isDuplicated)
                {
                    return Result.Fail(new PostError.ReportDuplicated(memberId.ToString()));
                }

                // #04. Add Post Report Entity
                var addedReport = post.AddPostReport(
                        command.ReportCategory,
                        command.Title,
                        command.HtmlBody,
                        memberId);

                // #04. Save to dbcontext
                await _postRepository.UnitOfWork.SaveEntitiesAsync(token);

                _logger.LogInformation("Creating Post Report - report: {@report}", addedReport);
                return addedReport.Id.ToString();
            }
        }
        #endregion

    }
}

 