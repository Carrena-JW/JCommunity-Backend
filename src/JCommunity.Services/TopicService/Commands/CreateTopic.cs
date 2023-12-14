using JCommunity.AppCore.Entities.Topics;

namespace JCommunity.Services.TopicService.Commands;

public class CreateTopic 
{
    public record Command : ICommand<Command, string>
    {
        #region [Command Parameters Model
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public int Sort { get; init; }
        public IEnumerable<string> Tags { get; init; } = Array.Empty<string>();
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
                RuleForEach(r => r.Tags)
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

            public Handler(ITopicRepository repository, ILogger<Handler> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            public async Task<Result<string>> Handle(Command command, CancellationToken ct)
            {
                var topic = Topic.Create(
                    command.Name,
                    command.Description,
                    command.Sort,
                    Guid.Parse(command.AuthorId));

                if(command.Tags.Count() > 0)
                {
                    
                }

                var isUniqueName = await _repository.IsUniqueTopicNameAsync(command.Name, ct);
                if (!isUniqueName) return Result.Fail(new TopicError.NameNotUnique(command.Name));


                _repository.Add(topic);
                await _repository.UnitOfWork.SaveChangesAsync(ct);

                return topic.Id.ToString();



            }
        }
        #endregion
    }
}



//public const int NAME_MIN_LENGTH = 2;
//public const int NAME_MAX_LENGTH = 20;

//public const int CATEGORY_NAME_MAX_LENGTH = 20;

//public const int DESCRIPTION_MAX_LENGTH = 100;