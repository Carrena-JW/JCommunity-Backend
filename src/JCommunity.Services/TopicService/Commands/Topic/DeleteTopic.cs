namespace JCommunity.Services.TopicService.Commands;

public class DeleteTopic
{
    public record Command : ICommand<Command, bool>
    {
        #region [Command Parameters Model
        public string Id { get; init; } = string.Empty;
       
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
                var findTopic = await _repository.GetTopicByIdAsync(Guid.Parse(command.Id));

                if (findTopic == null)
                {
                    return Result.Fail(new TopicError.NotFound(command.Id));
                }

                _repository.Remove(findTopic);
                 
                await _repository.UnitOfWork.SaveEntitiesAsync(ct);

                _logger.LogInformation("Delting Topic : {@findTopic}", findTopic);
                return true;

            }
        }
        #endregion
    }
}

 