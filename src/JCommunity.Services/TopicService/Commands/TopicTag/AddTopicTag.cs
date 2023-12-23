namespace JCommunity.Services.TopicService.Commands;

public class AddTopicTag
{
    public record Command : ICommand<Command, IEnumerable<TopicTag>>
    {
        #region [Command Parameters Model
        public string TopicId { get; init; } = string.Empty;
        public IEnumerable<string> Names { get; init; } = null!;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.TopicId).NotEmpty();

                RuleFor(r => r.Names).NotEmpty();

                RuleForEach(r => r.Names)
                    .NotEmpty()
                    .MaximumLength(TopicRestriction.TAG_NAME_MAX_LENGTH);
            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<IEnumerable<TopicTag>>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ITopicRepository _repository;

            public Handler(ITopicRepository repository, ILogger<Handler> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            public async Task<Result<IEnumerable<TopicTag>>> Handle(Command command, CancellationToken ct)
            {
                var options = TopicIncludeOptions.Create(true);
                var findTopic = await _repository.GetTopicByIdAsync(
                    Guid.Parse(command.TopicId), options, ct);

                if(findTopic == null)
                {
                    return Result.Fail(new TopicError.NotFound(command.TopicId));
                }

                var addedTags =  findTopic.AddTags(command.Names);

                await _repository.UnitOfWork.SaveEntitiesAsync(ct);

                _logger.LogInformation("Adding Topic Tag: {@topic}", addedTags);
                return Result.Ok(addedTags);

            }
        }
        #endregion
    }
}

