namespace JCommunity.Services.TopicService.Commands;

public class DeleteTopicTag
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model
        public string TopicId { get; init; } = string.Empty;
        public string TopicTagId { get; init; } = string.Empty;
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.TopicTagId)
                    .NotEmpty();

                RuleFor(r => r.TopicId)
                    .NotEmpty();

            }
        }
        #endregion

        #region [Handler]
        internal class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly ITopicRepository _repository;

            public Handler(ITopicRepository repository, ILogger<Handler> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            public async Task<Result<bool>> Handle(Command command, CancellationToken ct)
            {
                var options = TopicIncludeOptions.Create(true);
                var findTopic = await _repository.GetTopicByIdAsync(
                    command.TopicId.ConvertToUlid(), options, ct);

                if (findTopic == null)
                {
                    return Result.Fail(new TopicError.NotFound(command.TopicId));
                }

                var findTopicTag = findTopic.Tags
                    .Where(t => t.Id.ToString() == command.TopicTagId);

                if(findTopicTag.Count() == 0)
                {
                    return Result.Fail(new TopicError.TagNotFound(command.TopicTagId));
                }

                findTopic.RemoveTag(findTopicTag.SingleOrDefault()!);
                await _repository.UnitOfWork.SaveEntitiesAsync(ct);

                _logger.LogInformation("Deleting Topic Tag : {@topic}", findTopicTag);
                return true;

            }
        }
        #endregion
    }
}

 