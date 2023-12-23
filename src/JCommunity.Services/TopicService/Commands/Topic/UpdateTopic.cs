namespace JCommunity.Services.TopicService.Commands;

public class UpdateTopic
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model
        public string Id { get; init; } = string.Empty;
        public string? Name { get; init; }  
        public string? Description { get; init; } 
        public int? Sort { get; init; }
        public IEnumerable<string>? TagNames { get; init; }
        #endregion

        #region [Validator]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Name)
                   .MinimumLength(TopicRestriction.NAME_MIN_LENGTH)
                   .MaximumLength(TopicRestriction.NAME_MAX_LENGTH);

                RuleFor(r => r.Description)
                    .MaximumLength(TopicRestriction.DESCRIPTION_MAX_LENGTH);

                RuleForEach(r => r.TagNames)
                    .MaximumLength(TopicRestriction.TAG_NAME_MAX_LENGTH);

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

                var findTopic = await _repository.GetTopicByIdAsync(command.Id.ConvertToUlid(), options, ct);

                if (findTopic == null)
                {
                    return Result.Fail(new TopicError.NotFound(command.Id));
                }

                #region [Updating Values]
                if (!string.IsNullOrEmpty(command.Name) && command.Name != findTopic.Name)
                {
                    var isUnique = await _repository.IsUniqueTopicNameAsync(command.Name,ct);

                    if(!isUnique) return Result.Fail(new TopicError.NameNotUnique(command.Name));

                    findTopic.UpdateTopicName(command.Name);
                }

                if(!string.IsNullOrEmpty(command.Description))
                {
                    findTopic.UpdateTopicDescription(command.Description);
                }

                if(command.Sort.HasValue)
                {
                    findTopic.UpdateTopicSortOrder(command.Sort.Value);
                }

                if(command.TagNames != null) 
                {
                    findTopic.UpdateTags(command.TagNames);
                }

                findTopic.UpdateLastUpdateAt();

                #endregion

                await _repository.UnitOfWork.SaveEntitiesAsync(ct);


                _logger.LogInformation("Updating Topic : {@findTopic}", findTopic);
                return true;

            }
        }
        #endregion
    }
}

 