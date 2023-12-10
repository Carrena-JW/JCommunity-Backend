namespace JCommunity.AppCore.Core.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IRequestContract
    where TResponse : notnull
{
}
