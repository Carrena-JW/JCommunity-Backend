namespace JCommunity.Services.TopicService.Commands;

public class CreateTopic 
{
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public int Sort { get; init; }
        public IEnumerable<string> TagNames { get; init; } = Array.Empty<string>();
        public string AuthorId { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Name)
                    .NotEmpty()
                    .MinimumLength(TopicRestriction.NAME_MIN_LENGTH)
                    .MaximumLength(TopicRestriction.NAME_MAX_LENGTH);

                RuleFor(r => r.Description)
                    .MaximumLength(TopicRestriction.DESCRIPTION_MAX_LENGTH);

                // Tags Collection
                RuleForEach(r => r.TagNames)
                    .MaximumLength(TopicRestriction.TAG_NAME_MAX_LENGTH);

                RuleFor(r => r.AuthorId)
                    .NotEmpty().NotNull();
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ITopicRepository _repository;
            private readonly IMemberRepository _memberRepository;

            public Handler(
                ILogger<Handler> logger, 
                ITopicRepository repository, 
                IMemberRepository memberRepository)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken ct)
            {
                var IsExistsMemberAsync = await _memberRepository
                    .IsExistsMemberAsync(command.AuthorId.ConvertToUlid(), ct);

                if (!IsExistsMemberAsync)
                {
                    return Result.Fail(new MemberError.NotFound(command.AuthorId));
                }

                var topic = Topic.Create(
                    command.Name,
                    command.Description,
                    command.Sort,
                    command.AuthorId.ConvertToUlid());

                if(command.TagNames.Count() > 0)
                {
                    topic.AddTags(command.TagNames);
                }

                var isUniqueName = await _repository.IsUniqueTopicNameAsync(command.Name, ct);
                if (!isUniqueName) return Result.Fail(new TopicError.NameNotUnique(command.Name));


                _repository.Add(topic);
                await _repository.UnitOfWork.SaveEntitiesAsync(ct);

                _logger.LogInformation("Creating Topic : {@topic}", topic);
                return topic.Id.ToString();

            }
        }
        #endregion
    }
}

