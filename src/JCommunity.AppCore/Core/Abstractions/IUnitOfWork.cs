namespace JCommunity.AppCore.Core.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task<bool> SaveEntitiesAsync(CancellationToken ct = default);
}
