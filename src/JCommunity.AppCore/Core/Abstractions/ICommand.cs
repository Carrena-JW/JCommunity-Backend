namespace JCommunity.AppCore.Core.Abstractions;

public interface ICommand<TRequest, TResponse> : IRequest<Result<TResponse>>, IRequestContract
    where TRequest : notnull
    where TResponse : notnull
{
    public static TRequest CreateModel()
    {
        throw new NotImplementedException();
    }
}
