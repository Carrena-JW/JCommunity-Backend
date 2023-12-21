using JCommunity.AppCore.Core.BaseClasses;
using MediatR;

namespace JCommunity.Infrastructure;

public class AppDbContext : DbContext, IUnitOfWork
{

    public DbSet<Member> Members { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Post> Posts { get; set; }

    private readonly IMediator _mediator;

    public AppDbContext(
        DbContextOptions<AppDbContext> options, 
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken ct = default)
    {
        _ = await base.SaveChangesAsync(ct);

        var domainEntities = ChangeTracker
           .Entries<AggregateRoot>()
           .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);

        return true;
    }
}
